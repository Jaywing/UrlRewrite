using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates
{
    public class BaseEnabledItem : CustomItem
    {
        public static readonly string TemplateId = "{9CE37445-8572-4244-87E7-B0AAFBF87A35}";

        public BaseEnabledItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseEnabledItem(Item innerItem)
        {
            return innerItem != null ? new BaseEnabledItem(innerItem) : null;
        }

        public static implicit operator Item(BaseEnabledItem customItem)
        {
            return customItem?.InnerItem;
        }

        public CheckboxField Enabled => new CheckboxField(InnerItem.Fields["Enabled"]);
    }
}