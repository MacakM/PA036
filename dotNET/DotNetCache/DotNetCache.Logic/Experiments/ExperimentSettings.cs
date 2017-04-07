using System;
using EFCache;

namespace DotNetCache.Logic.Experiments
{
    public class ExperimentSettings
    {
        public double MaxCacheSizeInMegaBytes { get; set; }
        public int MaxCacheEntries { get; set; }
        public TimeSpan CachePurgeInterval { get; set; }
        public TimeSpan RelativeCacheEntryValidity { get; set; }

        public ExperimentSettings(double maxCacheSizeInMegaBytes, int maxCacheEntries, TimeSpan cachePurgeInterval, TimeSpan relativeCacheEntryValidity)
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
           
        }

        public static ExperimentSettings Default = new ExperimentSettings
        {
            MaxCacheSizeInMegaBytes = double.MaxValue,
            MaxCacheEntries = int.MaxValue,
            RelativeCacheEntryValidity = TimeSpan.MaxValue,
            CachePurgeInterval = TimeSpan.MaxValue
        };
    }
}
