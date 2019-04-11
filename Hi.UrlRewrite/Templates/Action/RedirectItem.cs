using Sitecore.Data.Items;
using Hi.UrlRewrite.Templates.Action.Base;

namespace Hi.UrlRewrite.Templates.Action
{
    public class RedirectItem : CustomItem
    {
        public const string TemplateId = "{D199EF8B-9D4D-420F-A283-E16D7B575625}";

        public BaseRedirectItem BaseRedirectItem { get; }

        public RedirectItem(Item innerItem)
            : base(innerItem)
        {
            BaseRedirectItem = new BaseRedirectItem(innerItem);

        }

        public static implicit operator RedirectItem(Item innerItem)
        {
            return innerItem != null ? new RedirectItem(innerItem) : null;
        }

        public static implicit operator Item(RedirectItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}