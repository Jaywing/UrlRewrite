using Sitecore.Data;
using System.Collections.Generic;
using Sitecore.Diagnostics;

namespace Hi.UrlRewrite.Caching
{
    public static class RulesCacheManager
    {
        private static readonly Dictionary<string, RulesCache> Caches = new Dictionary<string, RulesCache>();

        public static RulesCache GetCache(Database db)
        {
            Assert.IsNotNull(db, "Database (db) cannot be null.");

            if (Caches.ContainsKey(db.Name))
            {
                return Caches[db.Name];
            }

            var cache = new RulesCache(db);
            Caches.Add(db.Name, cache);

            return cache;
        }
    }
}