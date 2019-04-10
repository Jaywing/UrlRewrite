using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class AbortRequestItem : CustomItem
    {
        public const string TemplateId = "{BD8E6E5E-62F8-4397-99CA-B0502AFD14B9}";

        public BaseActionItem BaseActionItem { get; }

        public AbortRequestItem(Item innerItem)
            : base(innerItem)
        {
            BaseActionItem = new BaseActionItem(innerItem);
        }

        public static implicit operator AbortRequestItem(Item innerItem)
        {
            return innerItem != null ? new AbortRequestItem(innerItem) : null;
        }

        public static implicit operator Item(AbortRequestItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}