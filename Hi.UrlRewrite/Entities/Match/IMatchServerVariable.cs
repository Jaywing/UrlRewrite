namespace Hi.UrlRewrite.Entities.Match
{
    public interface IMatchServerVariable : IBaseMatchScope
    {
        string ServerVariableName { get; set; }
    }
}