using Hi.UrlRewrite.Entities.Rules;
using Sitecore;
using Sitecore.Caching;
using Sitecore.Data;
using System.Collections.Generic;
using System.Linq;

namespace Hi.UrlRewrite.Caching
{
    public class RulesCache : CustomCache
    {
        private const string InboundRulesKey = "InboundRules";
        private const string OutboundRulesKey = "OutboundRules";

        public RulesCache(Database db) :
            base($"Hi.UrlRewrite[{db.Name}]", StringUtil.ParseSizeString(Configuration.CacheSize))
        {
        }

        private List<T> GetRules<T>(string key) where T : IBaseRule
        {
            List<T> returnRules = null;
            if (InnerCache.GetValue(key) is IEnumerable<T> rules)
            {
                returnRules = rules.ToList();
            }

            return returnRules;
        }

        private void SetRules<T>(IEnumerable<T> outboundRules, string key) where T : IBaseRule
        {
            InnerCache.Add(key, outboundRules);
        }

        public List<InboundRule> GetInboundRules()
        {
            return GetRules<InboundRule>(InboundRulesKey);
        }

        public void SetInboundRules(IEnumerable<InboundRule> inboundRules)
        {
            SetRules(inboundRules, InboundRulesKey);
        }

        public List<OutboundRule> GetOutboundRules()
        {
            return GetRules<OutboundRule>(OutboundRulesKey);
        }

        public void SetOutboundRules(IEnumerable<OutboundRule> outboundRules)
        {
            SetRules(outboundRules, OutboundRulesKey);
        }
        
        public void ClearInboundRules()
        {
            RemoveKeysContaining(InboundRulesKey);
        }

        public void ClearOutboundRules()
        {
            RemoveKeysContaining(OutboundRulesKey);
        }
    }
}