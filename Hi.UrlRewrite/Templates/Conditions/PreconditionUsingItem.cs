using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Conditions
{
    public class PreconditionUsingItem : CustomItem
    {
        public const string TemplateId = "{20C923EF-DAD7-433E-B51C-A5B634C3A27B}";

        public PreconditionUsingItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator PreconditionUsingItem(Item innerItem)
        {
            return innerItem != null ? new PreconditionUsingItem(innerItem) : null;
        }

        public static implicit operator Item(PreconditionUsingItem customItem)
        {
            return customItem?.InnerItem;
        }
        public LookupField Using => new LookupField(InnerItem.Fields["Using"]);
    }
}