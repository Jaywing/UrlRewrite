﻿using System.Collections.Generic;
using Hi.UrlRewrite.Entities.ServerVariables;

namespace Hi.UrlRewrite.Processing.Results
{
    public class OutboundRuleResult : IRuleResult
    {
        public string OriginalResponseString { get; set; }
        public string RewrittenResponseString { get; set; }
        public ConditionMatchResult ConditionMatchResult { get; set; }
        public bool RuleMatched { get; set; }
        public bool StopProcessing { get; set; }
        public IEnumerable<ResponseHeader> ResponseHeaders { get; set; }

        public OutboundRuleResult()
        {
            ResponseHeaders = new List<ResponseHeader>();
        }
    }
}