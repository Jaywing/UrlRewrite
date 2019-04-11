using Hi.UrlRewrite.Templates.Conditions;
using Hi.UrlRewrite.Templates.Match;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates
{
    public class BaseRuleItem : CustomItem
    {
        public static readonly string TemplateId = "{995478B6-29FB-4CF8-B5FB-5C1C5B21BF5A}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public ConditionLogicalGroupingItem ConditionLogicalGroupingItem { get; }

        public BaseMatchItem BaseMatchItem { get; }

        public BaseEnabledItem BaseEnabledItem { get; }

        public BaseRuleItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
            ConditionLogicalGroupingItem = new ConditionLogicalGroupingItem(innerItem);
            BaseMatchItem = new BaseMatchItem(innerItem);
            BaseEnabledItem = new BaseEnabledItem(innerItem);
        }

        public static implicit operator BaseRuleItem(Item innerItem)
        {
            return innerItem != null ? new BaseRuleItem(innerItem) : null;
        }

        public static implicit operator Item(BaseRuleItem customItem)
        {
	        return customItem?.InnerItem;
        }
    }
}