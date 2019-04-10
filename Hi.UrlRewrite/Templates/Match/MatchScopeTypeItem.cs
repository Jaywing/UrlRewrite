using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchScopeTypeItem : CustomItem
    {
        public const string TemplateId = "{7B5B7C00-4708-485B-809E-1064B689B2DE}";

        public MatchScopeTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchScopeTypeItem(Item innerItem)
        {
            return innerItem != null ? new MatchScopeTypeItem(innerItem) : null;
        }

        public static implicit operator Item(MatchScopeTypeItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField MatchScopeType => new LookupField(InnerItem.Fields["Match Scope Type"]);
    }
}