using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseRewriteItem : CustomItem
    {
        public const string TemplateId = "{7E95E23F-437C-46DC-97AA-F2F6C79B78C1}";

        public BaseStopProcessingItem BaseStopProcessingItem { get; }

        public BaseAppendQuerystringItem BaseAppendQuerystringItem { get; }

        public BaseRewriteUrlItem BaseRewriteUrlItem { get; }

        public BaseRewriteItem(Item innerItem)
            : base(innerItem)
        {
            BaseStopProcessingItem = new BaseStopProcessingItem(innerItem);
            BaseAppendQuerystringItem = new BaseAppendQuerystringItem(innerItem);
            BaseRewriteUrlItem = new BaseRewriteUrlItem(innerItem);
        }

        public static implicit operator BaseRewriteItem(Item innerItem)
        {
            return innerItem != null ? new BaseRewriteItem(innerItem) : null;
        }

        public static implicit operator Item(BaseRewriteItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}