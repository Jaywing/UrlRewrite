using Hi.UrlRewrite.Entities.Actions;
using Hi.UrlRewrite.Entities.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hi.UrlRewrite.Processing.Results
{
    public class ProcessInboundRulesResult
    {
        public ProcessInboundRulesResult(Uri originalUri, List<InboundRuleResult> processedResults)
        {
            OriginalUri = originalUri;
            ProcessedResults = processedResults;
            InboundRuleResult lastMatchedResult = ProcessedResults.LastOrDefault(r => r.RuleMatched);

            if (lastMatchedResult != null)
            {
                RewrittenUri = lastMatchedResult.RewrittenUri;
                FinalAction = lastMatchedResult.ResultAction;
                ItemId = lastMatchedResult.ItemId;
            }
        }

        public Guid? ItemId { get; }

        public Uri OriginalUri { get; }

        public Uri RewrittenUri { get; }

        public IBaseAction FinalAction { get; }

        public List<InboundRuleResult> ProcessedResults { get; }

        public int? StatusCode 
        { 
            get
            {
                switch (FinalAction)
                {
                    case Redirect redirectAction when redirectAction.StatusCode.HasValue:
                        return (int) (redirectAction.StatusCode.Value);
                    case CustomResponse customResponse:
                        return customResponse.StatusCode;
                    default:
                        return null;
                }
            }
        }

        public bool MatchedAtLeastOneRule
        {
            get
            {
                return ProcessedResults != null && ProcessedResults.Any(e => e.RuleMatched);
            }
        }
    }
}