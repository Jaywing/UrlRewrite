using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseRedirectItem : CustomItem
    {
        public static readonly string TemplateId = "{D28318B2-5793-4ABA-BFB3-3C3FBC00AA92}";

        public BaseStopProcessingItem BaseStopProcessingItem { get; }

        public BaseAppendQuerystringItem BaseAppendQuerystringItem { get; }

        public BaseRewriteUrlItem BaseRewriteUrlItem { get; }

        public BaseRedirectTypeItem BaseRedirectTypeItem { get; }

        public BaseCacheItem BaseCacheItem { get; }

        public BaseRedirectItem(Item innerItem)
            : base(innerItem)
        {
            BaseStopProcessingItem = new BaseStopProcessingItem(innerItem);
            BaseAppendQuerystringItem = new BaseAppendQuerystringItem(innerItem);
            BaseRewriteUrlItem = new BaseRewriteUrlItem(innerItem);
            BaseRedirectTypeItem = new BaseRedirectTypeItem(innerItem);
            BaseCacheItem = new BaseCacheItem(innerItem);
        }

        public static implicit operator BaseRedirectItem(Item innerItem)
        {
            return innerItem != null ? new BaseRedirectItem(innerItem) : null;
        }

        public static implicit operator Item(BaseRedirectItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}