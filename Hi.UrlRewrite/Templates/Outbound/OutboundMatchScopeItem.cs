using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Outbound
{
    public class OutboundMatchScopeItem : CustomItem
    {
        public const string TemplateId = "{D14E8E19-04C6-4C98-B9FE-6830F6A957AF}";

        public OutboundMatchScopeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator OutboundMatchScopeItem(Item innerItem)
        {
            return innerItem != null ? new OutboundMatchScopeItem(innerItem) : null;
        }

        public static implicit operator Item(OutboundMatchScopeItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField MatchScope => new LookupField(InnerItem.Fields["Match Scope"]);
    }
}