using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Inbound
{
	public class InboundRuleItem : CustomItem
    {
        public const string TemplateId = "{69DCE9A6-D8C1-463D-AF95-B7FEB326013F}";

        public BaseRuleItem BaseRuleItem { get; }

        public InboundRuleItem(Item innerItem)
            : base(innerItem)
        {
            BaseRuleItem = new BaseRuleItem(innerItem);

        }

        public static implicit operator InboundRuleItem(Item innerItem)
        {
            return innerItem != null ? new InboundRuleItem(innerItem) : null;
        }

        public static implicit operator Item(InboundRuleItem customItem)
        {
	        return customItem?.InnerItem;
        }

        public LookupField Action => new LookupField(InnerItem.Fields["Action"]);

        public int SortOrder => this.InnerItem.Appearance.Sortorder;
    }
}