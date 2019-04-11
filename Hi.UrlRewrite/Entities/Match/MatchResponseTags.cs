using System;
using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.Match
{
    [Serializable]
    public class MatchResponseTags : IMatchResponseTags
    {
        public IEnumerable<MatchTag> MatchTheContentWithin { get; set; }

        public MatchResponseTags()
        {
            MatchTheContentWithin = new List<MatchTag>();
        }
    }
}