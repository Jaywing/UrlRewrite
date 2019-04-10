using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class BaseConditionItem : CustomItem
    {
        public const string TemplateId = "{1CDF3BDD-0A44-42CD-9D40-FBA83B981F48}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public BaseConditionItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
        }

        public static implicit operator BaseConditionItem(Item innerItem)
        {
            return innerItem != null ? new BaseConditionItem(innerItem) : null;
        }

        public static implicit operator Item(BaseConditionItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField CheckIfInputString => new LookupField(InnerItem.Fields["Check if input string"]);

        public TextField Pattern => new TextField(InnerItem.Fields["Pattern"]);
        
        public CheckboxField IgnoreCase => new CheckboxField(InnerItem.Fields["Ignore case"]);
    }
}