using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.ServerVariables
{
    internal interface IRequestHeaderList
    {
        IEnumerable<RequestHeader> RequestHeaders { get; set; }
    }
}