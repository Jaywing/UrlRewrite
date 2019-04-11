namespace Hi.UrlRewrite.Entities.Match
{
    public interface IOutboundMatchScope
    {
        IBaseMatchScope OutboundMatchScope { get; set; }
    }
}