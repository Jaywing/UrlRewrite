using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Types
{
    public class RedirectTypeItem : CustomItem
    {
        public const string TemplateId = "{5E020836-C778-4283-B199-82147C4C122F}";

        public RedirectTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator RedirectTypeItem(Item innerItem)
        {
            return innerItem != null ? new RedirectTypeItem(innerItem) : null;
        }

        public static implicit operator Item(RedirectTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}