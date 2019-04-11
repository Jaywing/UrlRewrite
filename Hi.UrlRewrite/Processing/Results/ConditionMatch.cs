using System.Text.RegularExpressions;

namespace Hi.UrlRewrite.Processing.Results
{
    public class ConditionMatch
    {
        public Match Match { get; set; }
        public string ConditionInput { get; set; }
    }
}