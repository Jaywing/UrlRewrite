using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class PreconditionItem : CustomItem
    {
        public const string TemplateId = "{A83E69AE-B93F-45C1-A20B-E6879BD04598}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public PreconditionUsingItem PreconditionUsingItem { get; }

        public ConditionLogicalGroupingItem ConditionLogicalGroupingItem { get; }

        public PreconditionItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
            PreconditionUsingItem = new PreconditionUsingItem(innerItem);
            ConditionLogicalGroupingItem = new ConditionLogicalGroupingItem(innerItem);
        }

        public static implicit operator PreconditionItem(Item innerItem)
        {
            return innerItem != null ? new PreconditionItem(innerItem) : null;
        }

        public static implicit operator Item(PreconditionItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}