﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using EFCache;

namespace DotNetCache.Logic.Experiments
{
    public class ExperimentSettings
    {
        public double MaxCacheSizeInMegaBytes { get; set; } = Double.MaxValue;
        public int MaxCacheEntries { get; set; } = Int32.MaxValue;
        public int CachePurgeInterval { get; set; } = Int32.MaxValue;
        public TimeSpan RelativeCacheEntryValidity { get; set; } = TimeSpan.MaxValue;
        public Dictionary<string, MetadataWorkspace> BlackList { get; set; } = new Dictionary<string, MetadataWorkspace>(); 

        public ExperimentSettings(double maxCacheSizeInMegaBytes, int maxCacheEntries, int cachePurgeInterval, TimeSpan relativeCacheEntryValidity)
        {
            MaxCacheSizeInMegaBytes = maxCacheSizeInMegaBytes;
            MaxCacheEntries = maxCacheEntries;
            CachePurgeInterval = cachePurgeInterval;
            RelativeCacheEntryValidity = relativeCacheEntryValidity;
        }

        public ExperimentSettings()
        {
        }

        public static void SetUpCache(ExperimentSettings setUp)
        {
            CachingPolicy.SlidingExpiration = setUp.RelativeCacheEntryValidity;
            InMemoryCache.EntryCountLimit = setUp.MaxCacheEntries;
            InMemoryCache.EntrySizeLimit = setUp.MaxCacheSizeInMegaBytes;
            InMemoryCache.PurgeSpan = setUp.CachePurgeInterval;
            foreach (var blacklistEntry in setUp.BlackList.Keys)
            {
                BlacklistedQueriesRegistrar.Instance.AddBlacklistedQuery(setUp.BlackList[blacklistEntry], blacklistEntry);
            }
        }

        public static ExperimentSettings Default = new ExperimentSettings
        {
            MaxCacheSizeInMegaBytes = double.MaxValue,
            MaxCacheEntries = int.MaxValue,
            RelativeCacheEntryValidity = TimeSpan.MaxValue,
            CachePurgeInterval = int.MaxValue
        };
    }
}
