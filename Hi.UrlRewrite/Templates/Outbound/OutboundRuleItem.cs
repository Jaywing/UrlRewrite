using Hi.UrlRewrite.Templates.Match;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Outbound
{
	public class OutboundRuleItem : CustomItem
	{
        public const string TemplateId = "{DC4D631D-E17D-4F18-A19F-3554CD4EB756}";

        public BaseRuleItem BaseRuleItem { get; }

        public OutboundPreconditionItem OutboundPreconditionItem { get; }

        public OutboundMatchItem OutboundMatchItem { get; }

        public OutboundRuleItem(Item innerItem)
			: base(innerItem)
		{
			BaseRuleItem = new BaseRuleItem(innerItem);
			OutboundPreconditionItem = new OutboundPreconditionItem(innerItem);
			OutboundMatchItem = new OutboundMatchItem(innerItem);
		}

		public static implicit operator OutboundRuleItem(Item innerItem)
		{
			return innerItem != null ? new OutboundRuleItem(innerItem) : null;
		}

		public static implicit operator Item(OutboundRuleItem customItem)
		{
			return customItem?.InnerItem;
		}

		public LookupField Action => new LookupField(InnerItem.Fields["Action"]);
    }
}