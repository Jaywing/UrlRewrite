using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.Match
{
    public interface IMatchResponseTags : IBaseMatchScope
    {
        IEnumerable<MatchTag> MatchTheContentWithin { get; set; }
    }
}