using System;

namespace Hi.UrlRewrite.Entities.ServerVariables
{
    [Serializable]
    public class RequestHeader : IBaseServerVariable
    {
        public string Name { get; set; }
        public string VariableName { get; set; }
        public string Value { get; set; }
        public bool ReplaceExistingValue { get; set; }
    }
}