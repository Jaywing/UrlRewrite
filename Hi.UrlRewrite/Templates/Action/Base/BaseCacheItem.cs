using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseCacheItem : CustomItem
    {
        public const string TemplateId = "{00C9EC91-5A01-4B46-AEE9-A040FCBA7777}";
        
        public BaseCacheItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseCacheItem(Item innerItem)
        {
            return innerItem != null ? new BaseCacheItem(innerItem) : null;
        }

        public static implicit operator Item(BaseCacheItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField HttpCacheability => new LookupField(InnerItem.Fields["HttpCacheability"]);
    }
}