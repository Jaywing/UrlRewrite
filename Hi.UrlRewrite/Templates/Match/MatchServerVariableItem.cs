using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchServerVariableItem : CustomItem
    {

        public const string TemplateId = "{31CBA307-BE81-4E55-96E4-62F70B2CBF47}";

        public MatchServerVariableItem(Item innerItem)
            : base(innerItem)
        {
        }

        public static implicit operator MatchServerVariableItem(Item innerItem)
        {
            return innerItem != null ? new MatchServerVariableItem(innerItem) : null;
        }

        public static implicit operator Item(MatchServerVariableItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField ServerVariableName => new TextField(InnerItem.Fields["Server Variable Name"]);
    }
}