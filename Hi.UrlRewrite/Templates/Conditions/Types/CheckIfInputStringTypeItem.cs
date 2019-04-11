using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Conditions.Types
{
    public class CheckIfInputStringTypeItem : CustomItem
    {
        public const string TemplateId = "{33B5817B-9BE3-4A97-8344-9096FEF5B8BC}";

        public CheckIfInputStringTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator CheckIfInputStringTypeItem(Item innerItem)
        {
            return innerItem != null ? new CheckIfInputStringTypeItem(innerItem) : null;
        }

        public static implicit operator Item(CheckIfInputStringTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}