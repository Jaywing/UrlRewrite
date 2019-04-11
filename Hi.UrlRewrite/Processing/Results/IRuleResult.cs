using Hi.UrlRewrite.Entities.ServerVariables;

namespace Hi.UrlRewrite.Processing.Results
{
    internal interface IRuleResult : IResponseHeaderList
    {
        ConditionMatchResult ConditionMatchResult { get; set; }
        bool RuleMatched { get; set; }
        bool StopProcessing { get; set; }
    }
}