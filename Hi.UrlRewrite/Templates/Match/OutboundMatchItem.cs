using Hi.UrlRewrite.Templates.Conditions;
using Hi.UrlRewrite.Templates.Outbound;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class OutboundMatchItem : CustomItem
    {
        public const string TemplateId = "{55E021F5-E48A-4340-AC93-8861BBE3C104}";

        public BaseMatchItem BaseMatchItem { get; }

        public MatchScopeTypeItem MatchScopeItem { get; }

        public OutboundMatchScopeItem OutboundMatchScopeItem { get; }

        public OutboundMatchItem(Item innerItem)
            : base(innerItem)
        {
            BaseMatchItem = new BaseMatchItem(innerItem);
            MatchScopeItem = new MatchScopeTypeItem(innerItem);
            OutboundMatchScopeItem = new OutboundMatchScopeItem(innerItem);
        }

        public static implicit operator OutboundMatchItem(Item innerItem)
        {
            return innerItem != null ? new OutboundMatchItem(innerItem) : null;
        }

        public static implicit operator Item(OutboundMatchItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}