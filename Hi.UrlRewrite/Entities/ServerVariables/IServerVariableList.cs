using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.ServerVariables
{
    internal interface IServerVariableList
    {
        IEnumerable<ServerVariable> ServerVariables { get; set; }
    }
}