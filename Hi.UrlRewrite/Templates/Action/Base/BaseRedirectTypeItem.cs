using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseRedirectTypeItem : CustomItem
    {
        public const string TemplateId = "{2372E073-6219-4F31-A7B0-DDA295636A6A}s";

        public BaseRedirectTypeItem(Item innerItem)
            : base(innerItem)
        {
        }

        public static implicit operator BaseRedirectTypeItem(Item innerItem)
        {
            return innerItem != null ? new BaseRedirectTypeItem(innerItem) : null;
        }

        public static implicit operator Item(BaseRedirectTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
        public LookupField RedirectType => new LookupField(InnerItem.Fields["Redirect type"]);
    }
}