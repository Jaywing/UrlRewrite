using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates
{
    public class BaseUrlRewriteItem : CustomItem
    {
        public const string TemplateId = "{83AFF04D-C0DA-44D4-8A7E-4BC0A89903E8}";

        public BaseUrlRewriteItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseUrlRewriteItem(Item innerItem)
        {
            return innerItem != null ? new BaseUrlRewriteItem(innerItem) : null;
        }

        public static implicit operator Item(BaseUrlRewriteItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}