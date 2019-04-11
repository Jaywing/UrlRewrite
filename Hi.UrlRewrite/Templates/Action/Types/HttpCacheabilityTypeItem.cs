using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Types
{
    public class HttpCacheabilityTypeItem : CustomItem
    {
        public const string TemplateId = "{482F740A-FEF3-4C88-AE89-3D5859FD0D6D}";
        
        public HttpCacheabilityTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator HttpCacheabilityTypeItem(Item innerItem)
        {
            return innerItem != null ? new HttpCacheabilityTypeItem(innerItem) : null;
        }

        public static implicit operator Item(HttpCacheabilityTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}