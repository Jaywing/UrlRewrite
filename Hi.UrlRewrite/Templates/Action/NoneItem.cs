using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class NoneItem : CustomItem
    {
        public const string TemplateId = "{12FA1A86-77CC-4097-86DB-A66849AF157A}";

        public BaseStopProcessingItem BaseStopProcessingActionItem { get; }

        public NoneItem(Item innerItem)
            : base(innerItem)
        {
            BaseStopProcessingActionItem = new BaseStopProcessingItem(innerItem);
        }

        public static implicit operator NoneItem(Item innerItem)
        {
            return innerItem != null ? new NoneItem(innerItem) : null;
        }

        public static implicit operator Item(NoneItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}