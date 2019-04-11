using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.ServerVariables
{
    public class BaseServerVariableItem : CustomItem
    {
        public const string TemplateId = "{ED218F0C-2AF5-4D0A-AA45-E089F9862E0C}";

        public BaseServerVariableItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseServerVariableItem(Item innerItem)
        {
            return innerItem != null ? new BaseServerVariableItem(innerItem) : null;
        }

        public static implicit operator Item(BaseServerVariableItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField VariableName => new TextField(InnerItem.Fields["Variable Name"]);

        public TextField Value => new TextField(InnerItem.Fields["Value"]);

        public CheckboxField ReplaceExistingValue => new CheckboxField(InnerItem.Fields["Replace existing value"]);
    }
}