// Copyright (c) Pawel Kadluczka, Inc. All rights reserved. See License.txt in the project root for license information.

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;

namespace EFCache
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    [Serializable]
    public class InMemoryCache : ICache
    {
        public Dictionary<string, CacheEntry> _cache { get; } = new Dictionary<string, CacheEntry>();

        private readonly Dictionary<string, HashSet<string>> _entitySetToKey =
            new Dictionary<string, HashSet<string>>();

        public static bool LastCached { get; private set; }
        public static bool WasCached { get; private set; }
        public static double EntryCountLimit { get; set; }
        public static double EntrySizeLimit { get; set; }
        public static double CacheSizeInMb { get; set; }
        public static long RealEntryCount { get; set; }
        private static int _purgeSpan = Int32.MaxValue;

        public static int PurgeSpan
        {
            get { return _purgeSpan; }
            set
            {
                if (value <= 0)
                {
                    _purgeSpan = 1;
                }
                _purgeSpan = value;
            }
        }

        public InMemoryCache()
        {
            PurgePeriodically();
        }

        public void ClearCache()
        {
            LastCached = false;
            WasCached = false;
            _cache.Clear();
            _entitySetToKey.Clear();
            CacheSizeInMb = 0;
            RealEntryCount = 0;
        }

        public List<string> GetKeys()
        {
            return _cache.Keys.ToList();
        }

        public bool GetItem(string key, out object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            value = null;

            lock (_cache)
            {
                var now = DateTimeOffset.Now;

                CacheEntry entry;
                if (_cache.TryGetValue(key, out entry))
                {
                    if (EntryExpired(entry, now))
                    {
                        InvalidateItem(key);
                    }
                    else
                    {
                        entry.LastAccess = now;
                        value = entry.Value;
                        LastCached = true;
                        return true;
                    }
                }
            }
            LastCached = false;
            return false;
        }



        public void PurgePeriodically()
        {
            Timer mtimer = new Timer();
            mtimer.Interval = PurgeSpan; //1ms
            mtimer.Elapsed += Purge;
            mtimer.Start();
        }

        private int x = 0;

        private void Purge(object sender, ElapsedEventArgs e)
        {
            x++;
            Purge();
        }


        public static void Decrease(CacheEntry entry, bool decreace = true)
        {
            long size = 0;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, entry);
                size = s.Length;
                if (decreace)
                {
                    CacheSizeInMb -= (double)size / 1000000;
                }
                else
                {
                    CacheSizeInMb += (double)size / 1000000;
                }
            }
        }

        public void PutItem(string key, object value, IEnumerable<string> dependentEntitySets, TimeSpan slidingExpiration, DateTimeOffset absoluteExpiration)
        {
            if (RealEntryCount >= EntryCountLimit || !Compare(CacheSizeInMb, EntrySizeLimit) || EntrySizeLimit == 0)
            {
                WasCached = false;
                return;
            }
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (dependentEntitySets == null)
            {
                throw new ArgumentNullException("dependentEntitySets");
            }
            WasCached = true;

            lock (_cache)
            {
                var entitySets = dependentEntitySets.ToArray();

                _cache[key] = new CacheEntry(value, entitySets , slidingExpiration, absoluteExpiration);
                Decrease(_cache[key],false);
                foreach (var entitySet in entitySets)
                {
                    HashSet<string> keys;

                    if (!_entitySetToKey.TryGetValue(entitySet, out keys))
                    {
                        keys = new HashSet<string>();
                        _entitySetToKey[entitySet] = keys;
                    }

                    keys.Add(key);                    
                }
            }
        }

        private bool Compare(double size, double limit)
        {
            return  size <= limit;
        }

        public void InvalidateSets(IEnumerable<string> entitySets)
        {
            if (entitySets == null)
            {
                throw new ArgumentNullException("entitySets");
            }
            
            lock (_cache)
            {
                var itemsToInvalidate = new HashSet<string>();

                foreach (var entitySet in entitySets)
                {
                    HashSet<string> keys;

                    if (_entitySetToKey.TryGetValue(entitySet, out keys))
                    {
                        itemsToInvalidate.UnionWith(keys);

                        _entitySetToKey.Remove(entitySet);
                    }
                }

                foreach (var key in itemsToInvalidate)
                {
                    InvalidateItem(key);
                }
            }
        }

        public void InvalidateItem(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            lock (_cache)
            {
                CacheEntry entry;

                if (_cache.TryGetValue(key, out entry))
                {
                    Decrease(_cache[key]);
                    _cache.Remove(key);

                    foreach (var set in entry.EntitySets)
                    {
                        HashSet<string> keys;
                        if (_entitySetToKey.TryGetValue(set, out keys))
                        {
                            keys.Remove(key);
                        }
                    }
                }
            }
        }

        public void Purge()
        {
            lock (_cache)
            {
                var now = DateTimeOffset.Now;
                var itemsToRemove = new HashSet<string>();

                foreach (var item in _cache)
                {
                    if (EntryExpired(item.Value, now))
                    {
                        itemsToRemove.Add(item.Key);
                    }
                }

                foreach (var key in itemsToRemove)
                {
                    InvalidateItem(key);
                }
            }
        }

        public int Count
        {
            get { return _cache.Count; }
        }

        private static bool EntryExpired(CacheEntry entry, DateTimeOffset now)
        {
            return entry.AbsoluteExpiration < now || (now - entry.LastAccess) > entry.SlidingExpiration;
        }

        [Serializable]
        public class CacheEntry
        {
            private readonly object _value;
            private readonly string[] _entitySets;
            private readonly TimeSpan _slidingExpiration;
            private readonly DateTimeOffset _absoluteExpiration;

            public CacheEntry(object value, string[] entitySets, TimeSpan slidingExpiration,
                DateTimeOffset absoluteExpiration)
            {
                _value = value;
                _entitySets = entitySets;
                _slidingExpiration = slidingExpiration;
                _absoluteExpiration = absoluteExpiration;
                LastAccess = DateTimeOffset.Now;
            }

            public object Value
            {
                get { return _value; }
            }

            public string[] EntitySets
            {
                get { return _entitySets; }
            }

            public TimeSpan SlidingExpiration
            {
                get { return _slidingExpiration; }
            }

            public DateTimeOffset AbsoluteExpiration
            {
                get { return _absoluteExpiration; }
            }

            public DateTimeOffset LastAccess { get; set; }
        }
    }
}
