using Hi.UrlRewrite.Entities.Actions;
using Hi.UrlRewrite.Entities.Match;
using Hi.UrlRewrite.Entities.Rules;
using Hi.UrlRewrite.Processing.Results;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Hi.UrlRewrite.Entities.Conditions;

namespace Hi.UrlRewrite.Processing
{
    public class OutboundRewriter
    {
        public NameValueCollection RequestServerVariables { get; set; }
        public NameValueCollection RequestHeaders { get; set; }
        public NameValueCollection ResponseHeaders { get; set; }

        public OutboundRewriter()
        {
            RequestServerVariables = new NameValueCollection();
            RequestHeaders = new NameValueCollection();
            ResponseHeaders = new NameValueCollection();
        }

        public void SetupReplacements(NameValueCollection requestServerVariables, NameValueCollection requestHeaders, NameValueCollection responseHeaders)
        {
            RequestServerVariables = requestServerVariables;
            RequestHeaders = requestHeaders;
            ResponseHeaders = responseHeaders;
        }

        public ProcessOutboundRulesResult ProcessContext(HttpContextBase httpContext, string responseString, IEnumerable<OutboundRule> outboundRules)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            if (outboundRules == null) throw new ArgumentNullException(nameof(outboundRules));

            // process outbound rules here... only set up event if it matches rules and preconditions

            var processedResults = new List<OutboundRuleResult>();
            var ruleResult = new OutboundRuleResult()
            {
                RewrittenResponseString = responseString
            };

            foreach (OutboundRule outboundRule in outboundRules)
            {
                ruleResult = ProcessOutboundRule(httpContext, ruleResult.RewrittenResponseString, outboundRule);
                processedResults.Add(ruleResult);

                if (!ruleResult.RuleMatched)
                    continue;

                if (ruleResult.RuleMatched && ruleResult.StopProcessing)
                {
                    break;
                }
            }

            // check rule matches

            // check conditions

            var result = new ProcessOutboundRulesResult(processedResults);

            return result;
        }

        private OutboundRuleResult ProcessOutboundRule(HttpContextBase httpContext, string responseString, OutboundRule outboundRule)
        {
            var ruleResult = new OutboundRuleResult()
            {
                OriginalResponseString = responseString,
                RewrittenResponseString = responseString
            };

            switch (outboundRule.Using)
            {
                case Using.ExactMatch:
                case Using.RegularExpressions:
                case Using.Wildcards:
                    ruleResult = ProcessRegularExpressionOutboundRule(ruleResult, outboundRule);
                    break;
                //case Using.Wildcards:
                //    //TODO: Implement Wildcards
                //    throw new NotImplementedException("Using Wildcards has not been implemented");
                //    break;
            }

            return ruleResult;
        }

        private OutboundRuleResult ProcessRegularExpressionOutboundRule(OutboundRuleResult ruleResult, OutboundRule outboundRule)
        {
            Match outboundRuleMatch, lastConditionMatch = null;

            // test rule match
            var isRuleMatch = true;

            // test conditions matches
            if (outboundRule.Conditions != null && outboundRule.Conditions.Any())
            {
                var replacements = new RewriteHelper.Replacements
                {
                    RequestHeaders = RequestHeaders,
                    RequestServerVariables = RequestServerVariables,
                    ResponseHeaders = ResponseHeaders
                };

                ConditionMatchResult conditionMatchResult = RewriteHelper.TestConditionMatches(outboundRule, replacements, out lastConditionMatch);
                isRuleMatch = conditionMatchResult.Matched;
            }

            if (isRuleMatch)
            {
                ruleResult.RewrittenResponseString = ProcessRuleReplacements(ruleResult.OriginalResponseString, outboundRule);
                ruleResult.RuleMatched = true;
            }

            return ruleResult;
        }

        public static string ProcessRuleReplacements(string responseString, OutboundRule outboundRule)
        {
            string output = null;
            string rewritePattern = outboundRule.Pattern;

            // TODO: Not all actions will be OutboundRewriteActions - fix this
            var rewrite = ((OutboundRewrite)outboundRule.Action);
            string rewriteValue = rewrite.Value;
            IBaseMatchScope rewriteMatchScope = outboundRule.OutboundMatchScope;
            ScopeType rewriteMatchScopeType = outboundRule.MatchingScopeType;

            // TODO: catch invalid Regex compilations
            if (rewriteMatchScopeType == ScopeType.Response)
            {
                IEnumerable<MatchTag> matchTags = new List<MatchTag>();

                if (rewriteMatchScope is MatchResponseTags)
                {
                    var matchResponseTags = rewriteMatchScope as MatchResponseTags;
                    matchTags = matchResponseTags.MatchTheContentWithin ?? new List<MatchTag>();
                }

                // if we are not matching on match tags, then we are doing matching on the entire response
                if (matchTags.Any())
                {
                    output = ProcessRuleReplacementsWithMatchTags(responseString, outboundRule.Using, matchTags,
                        rewritePattern, rewriteValue);
                }
                else
                {
                    if (outboundRule.Using == Using.ExactMatch)
                    {
                        output = responseString.Replace(rewritePattern, rewriteValue);
                    }
                    else
                    {
                        var responseRegex = new Regex(rewritePattern);

                        output = responseRegex.Replace(responseString,
                            match => RewriteHelper.ReplaceRuleBackReferences(match, rewriteValue));
                    }
                }
            }
            else if (rewriteMatchScopeType == ScopeType.ServerVariables)
            {

            }

            return output;
        }

        private static string ProcessRuleReplacementsWithMatchTags(string responseString, Using? outboundRuleUsing,
            IEnumerable<MatchTag> matchTags, string rewritePattern, string rewriteValue)
        {
            const string startKey = "start";
            const string innerKey = "inner";
            const string endKey = "end";
            const string nameKey = "name";
            const string startquoteKey = "startquote";
            const string valueKey = "value";
            const string endquoteKey = "endquote";

            const string tagPatternFormat =
                @"(?<" + startKey + @"><{0}\s+)(?<" + innerKey + @">.*?{1}=(?:""|').*?)(?<" + endKey + @">\s*/?>)";

            const string attributePatternFormat =
                @"(?<" + nameKey + @">{0}=)(?<" + startquoteKey + @">""|')(?<" + valueKey + @">.*?)(?<" +
                endquoteKey + @">""|')";

            string output = responseString;

            foreach (MatchTag matchTag in matchTags)
            {
                string tag = matchTag.Tag;
                string attribute = matchTag.Attribute;
                string tagPattern = string.Format(tagPatternFormat, tag, attribute);
                var tagRegex = new Regex(tagPattern);

                output = tagRegex.Replace(responseString, tagMatch =>
                {
                    GroupCollection tagMatchGroups = tagMatch.Groups;
                    string tagStart = tagMatchGroups[startKey].Value;
                    string tagInnards = tagMatchGroups[innerKey].Value;
                    string tagEnd = tagMatchGroups[endKey].Value;
                    string attributePattern = string.Format(attributePatternFormat, attribute);
                    var attributeRegex = new Regex(attributePattern);

                    string newTagInnards = attributeRegex.Replace(tagInnards, attributeMatch =>
                    {
                        GroupCollection attributeMatchGroups = attributeMatch.Groups;
                        string attributeValue = attributeMatchGroups[valueKey].Value;

                        var attributeValueRegex = new Regex(rewritePattern);
                        Match attributeValueMatch = attributeValueRegex.Match(attributeValue);

                        if (attributeValueMatch.Success)
                        {
                            string attributeName = attributeMatchGroups[nameKey].Value;
                            string attributeStartQuote = attributeMatchGroups[startquoteKey].Value;
                            string attributeEndQuote = attributeMatchGroups[endquoteKey].Value;

                            // need to determine where the match occurs within the original string
                            int attributeValueMatchIndex = attributeValueMatch.Index;
                            int attributeValueMatchLength = attributeValueMatch.Length;
                            string attributeValueReplaced;

                            if (outboundRuleUsing == Using.ExactMatch)
                            {
                                attributeValueReplaced = attributeValueMatch.Value.Replace(
                                    attributeValueMatch.Value, rewriteValue);
                            }
                            else
                            {
                                attributeValueReplaced = RewriteHelper.ReplaceRuleBackReferences(attributeValueMatch,
                                    rewriteValue);
                            }

                            string newAttributeValue = attributeValue.Substring(0, attributeValueMatchIndex) +
                                                       attributeValueReplaced +
                                                       attributeValue.Substring(attributeValueMatchIndex +
                                                                                attributeValueMatchLength);

                            string attributeOutput = attributeName + attributeStartQuote + newAttributeValue +
                                                  attributeEndQuote;

                            return attributeOutput;
                        }

                        return attributeMatch.Value;
                    });

                    string tagOutput = tagStart + newTagInnards + tagEnd;

                    return tagOutput;
                });
            }
            return output;
        }

        internal PreconditionResult CheckPreconditions(HttpContextBase httpContext, List<OutboundRule> outboundRules)
        {
            var isPreconditionMatch = false;

            IEnumerable<Precondition> preconditions = outboundRules.Select(p => p.Precondition).Where(p => p != null);

            foreach (Precondition precondition in preconditions)
            {
                IEnumerable<Condition> conditions = precondition.Conditions;

                // test conditions matches
                if (conditions != null && conditions.Any())
                {
                    var replacements = new RewriteHelper.Replacements
                    {
                        RequestServerVariables = httpContext.Request.ServerVariables,
                        RequestHeaders = httpContext.Request.Headers,
                        ResponseHeaders = httpContext.Response.Headers
                    };
                    ConditionMatchResult conditionMatchResult = RewriteHelper.TestConditionMatches(precondition, replacements, out Match _);
                    isPreconditionMatch = conditionMatchResult.Matched;
                }
            }

            var result = new PreconditionResult { Passed = isPreconditionMatch };

            return result;
        }

        internal bool CheckPrecondition(HttpContextBase httpContext, OutboundRule outboundRule)
        {
            var isPreconditionMatch = true;

            if (outboundRule == null) return isPreconditionMatch;
            Precondition precondition = outboundRule.Precondition;

            if (precondition == null) return isPreconditionMatch;
            IEnumerable<Condition> conditions = precondition.Conditions;

            // test conditions matches
            if (conditions != null && conditions.Any())
            {
                var replacements = new RewriteHelper.Replacements
                {
                    RequestServerVariables = httpContext.Request.ServerVariables,
                    RequestHeaders = httpContext.Request.Headers,
                    ResponseHeaders = httpContext.Response.Headers
                };
                ConditionMatchResult conditionMatchResult = RewriteHelper.TestConditionMatches(precondition, replacements, out Match _);
                isPreconditionMatch = conditionMatchResult.Matched;
            }

            return isPreconditionMatch;
        }
    }
}