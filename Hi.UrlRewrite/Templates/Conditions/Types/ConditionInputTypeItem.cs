using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Conditions.Types
{
    public class ConditionInputTypeItem : CustomItem
    {
        public const string TemplateId = "{01A28131-5CC9-4419-BD14-351922CE9416}";
        
        public ConditionInputTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator ConditionInputTypeItem(Item innerItem)
        {
            return innerItem != null ? new ConditionInputTypeItem(innerItem) : null;
        }

        public static implicit operator Item(ConditionInputTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}