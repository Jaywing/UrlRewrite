using System.Collections.Generic;
using System.Linq;

namespace Hi.UrlRewrite.Processing.Results
{
    public class ProcessOutboundRulesResult
    {
        public ProcessOutboundRulesResult(List<OutboundRuleResult> processedResults)
        {
            ProcessedResults = processedResults;
            OutboundRuleResult lastMatchedResult = ProcessedResults.LastOrDefault(r => r.RuleMatched);

            if (lastMatchedResult != null)
            {
                ResponseString = lastMatchedResult.RewrittenResponseString;
            }
        }

        public string ResponseString { get; }
        private List<OutboundRuleResult> ProcessedResults { get; }

        public bool MatchedAtLeastOneRule
        {
            get
            {
                return ProcessedResults != null && ProcessedResults.Any(e => e.RuleMatched);
            }
        }
    }
}