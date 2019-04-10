using Sitecore.Data.Items;
using Sitecore.Data.Fields;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseStopProcessingItem : CustomItem
    {
        public const string TemplateId = "{05BB43CA-F36D-46CF-BBAB-D46C8E3FEF16}";

        public BaseActionItem BaseAction { get; }

        public BaseStopProcessingItem(Item innerItem)
            : base(innerItem)
        {
            BaseAction = new BaseActionItem(innerItem);

        }

        public static implicit operator BaseStopProcessingItem(Item innerItem)
        {
            return innerItem != null ? new BaseStopProcessingItem(innerItem) : null;
        }

        public static implicit operator Item(BaseStopProcessingItem customItem)
        {
            return customItem?.InnerItem;
        }

        public CheckboxField StopProcessingOfSubsequentRules => new CheckboxField(InnerItem.Fields["Stop processing of subsequent rules"]);
    }
}