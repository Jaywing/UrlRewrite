using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class ConditionAdvancedItem : CustomItem
    {
        public const string TemplateId = "{0035B824-507B-4F69-9A1E-FFC2DD738D2D}";

        public BaseConditionItem BaseConditionItem { get; }

        public ConditionAdvancedItem(Item innerItem)
            : base(innerItem)
        {
            BaseConditionItem = new BaseConditionItem(innerItem);
        }

        public static implicit operator ConditionAdvancedItem(Item innerItem)
        {
            return innerItem != null ? new ConditionAdvancedItem(innerItem) : null;
        }

        public static implicit operator Item(ConditionAdvancedItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField ConditionInputString => new TextField(InnerItem.Fields["Condition Input String"]);
    }
}