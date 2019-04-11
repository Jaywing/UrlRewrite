using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchUsingItem : CustomItem
    {
        public const string TemplateId = "{BD62F334-3EAB-483D-A88B-D1D36EE51C9E}";

        public MatchUsingItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchUsingItem(Item innerItem)
        {
            return innerItem != null ? new MatchUsingItem(innerItem) : null;
        }

        public static implicit operator Item(MatchUsingItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField Using => new LookupField(InnerItem.Fields["Using"]);
    }
}