using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class OutboundRewriteItem : CustomItem
    {
        public const string TemplateId = "{A5E7B43A-4387-4109-950E-6181452F4976}";

        public BaseStopProcessingItem BaseStopProcessingItem { get; }

        public OutboundRewriteItem(Item innerItem)
            : base(innerItem)
        {
            BaseStopProcessingItem = new BaseStopProcessingItem(innerItem);
        }

        public static implicit operator OutboundRewriteItem(Item innerItem)
        {
            return innerItem != null ? new OutboundRewriteItem(innerItem) : null;
        }

        public static implicit operator Item(OutboundRewriteItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField Value => new TextField(InnerItem.Fields["Value"]);
    }
}