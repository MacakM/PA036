using System.Data.Entity;
using System.Data.Entity.Core.Common;
using EFCache;

namespace DotNetCache.DataAccess.Cache
{
    public class CacheConfiguration : DbConfiguration
    {
        public CacheConfiguration()
        {
            var transactionHandler = new CacheTransactionHandler(new InMemoryCache());

            AddInterceptor(transactionHandler);

            Loaded +=
                (sender, args) => args.ReplaceService<DbProviderServices>(
                    (s, _) => new CachingProviderServices(s, transactionHandler));
        }
    }
}