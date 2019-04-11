using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseAppendQuerystringItem : CustomItem
    {
        public const string TemplateId = "{DA9B513D-D4FA-4FC3-A49D-EB6754C37DD1}";
        
        public BaseAppendQuerystringItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseAppendQuerystringItem(Item innerItem)
        {
            return innerItem != null ? new BaseAppendQuerystringItem(innerItem) : null;
        }

        public static implicit operator Item(BaseAppendQuerystringItem customItem)
        {
            return customItem?.InnerItem;
        }
        
        public CheckboxField AppendQueryString => new CheckboxField(InnerItem.Fields["Append query string"]);
    }
}