using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match.Types
{
    public class UsingTypeItem : CustomItem
    {
        public const string TemplateId = "{7CBECFAE-2E9E-4E79-A6ED-02536EAFA383}";
        
        public UsingTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator UsingTypeItem(Item innerItem)
        {
            return innerItem != null ? new UsingTypeItem(innerItem) : null;
        }

        public static implicit operator Item(UsingTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}