using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Conditions.Types
{
    public class LogicalGroupingTypeItem : CustomItem
    {
        public const string TemplateId = "{075E8F5E-04A4-404D-A780-939148E7FB45}";
        
        public LogicalGroupingTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator LogicalGroupingTypeItem(Item innerItem)
        {
            return innerItem != null ? new LogicalGroupingTypeItem(innerItem) : null;
        }

        public static implicit operator Item(LogicalGroupingTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}