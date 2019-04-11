using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class ItemQueryRedirectItem : CustomItem
    {
        public const string TemplateId = "{5B7FB661-CB6C-449C-9C84-2672538AC77C}";

        public BaseAppendQuerystringItem BaseAppendQuerystringItem { get; }

        public BaseCacheItem BaseCacheItem { get; }

        public BaseRedirectTypeItem BaseRedirectTypeItem { get; }

        public BaseStopProcessingItem BaseStopProcessingItem { get; }

        public ItemQueryRedirectItem(Item innerItem)
            : base(innerItem)
        {
            BaseAppendQuerystringItem = new BaseAppendQuerystringItem(innerItem);
            BaseCacheItem = new BaseCacheItem(innerItem);
            BaseRedirectTypeItem = new BaseRedirectTypeItem(innerItem);
            BaseStopProcessingItem = new BaseStopProcessingItem(innerItem);
        }

        public static implicit operator ItemQueryRedirectItem(Item innerItem)
        {
            return innerItem != null ? new ItemQueryRedirectItem(innerItem) : null;
        }

        public static implicit operator Item(ItemQueryRedirectItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField ItemQuery => new TextField(InnerItem.Fields["Item Query"]);
    }
}