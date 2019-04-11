using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.ServerVariables
{
    internal interface IResponseHeaderList
    {
        IEnumerable<ResponseHeader> ResponseHeaders { get; set; }
    }
}