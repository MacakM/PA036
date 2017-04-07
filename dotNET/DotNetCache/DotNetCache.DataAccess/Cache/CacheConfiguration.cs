using System.Data.Entity;
using System.Data.Entity.Core.Common;
using DotNetCache.DataAccess.DemoDataContext;
using EFCache;

namespace DotNetCache.DataAccess.Cache
{
    public class CacheConfiguration : DbConfiguration
    {
        public CacheConfiguration()
        {
            var transactionHandler = new CacheTransactionHandler(DemoDataDbContext.Cache);

            AddInterceptor(transactionHandler);

            Loaded +=
                (sender, args) => args.ReplaceService<DbProviderServices>(
                    (s, _) => new CachingProviderServices(s, transactionHandler));
        }
    }
}