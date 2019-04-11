using System;

namespace Hi.UrlRewrite.Entities.Match
{
    [Serializable]
    public class MatchServerVariable : IMatchServerVariable
    {
        public string ServerVariableName { get; set; }
    }
}