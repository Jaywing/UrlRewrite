using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class ConditionLogicalGroupingItem : CustomItem
    {
        public const string TemplateId = "{1652DCC0-6319-43D4-853F-B5A441866F86}";
        
        public ConditionLogicalGroupingItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator ConditionLogicalGroupingItem(Item innerItem)
        {
            return innerItem != null ? new ConditionLogicalGroupingItem(innerItem) : null;
        }

        public static implicit operator Item(ConditionLogicalGroupingItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField LogicalGrouping => new LookupField(InnerItem.Fields["Logical grouping"]);
    }
}