using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseActionItem : CustomItem
    {
        public const string TemplateId = "{5C9153F1-6CDD-40C7-9670-1D1DCA23E784}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public BaseActionItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);

        }

        public static implicit operator BaseActionItem(Item innerItem)
        {
            return innerItem != null ? new BaseActionItem(innerItem) : null;
        }

        public static implicit operator Item(BaseActionItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}