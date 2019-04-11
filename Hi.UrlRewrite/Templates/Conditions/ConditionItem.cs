using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class ConditionItem : CustomItem
    {
        public const string TemplateId = "{2083F66B-0A94-4F9C-9833-EF53FAD05D70}";

        public BaseConditionItem BaseConditionItem { get; }

        public ConditionItem(Item innerItem)
            : base(innerItem)
        {
            BaseConditionItem = new BaseConditionItem(innerItem);
        }

        public static implicit operator ConditionItem(Item innerItem)
        {
            return innerItem != null ? new ConditionItem(innerItem) : null;
        }

        public static implicit operator Item(ConditionItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField ConditionInputType => new LookupField(InnerItem.Fields["Condition Input Type"]);
    }
}