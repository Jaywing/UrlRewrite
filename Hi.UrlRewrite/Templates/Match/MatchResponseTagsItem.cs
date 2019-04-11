using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchResponseTagsItem : CustomItem
    {

        public const string TemplateId = "{E07F2DBA-75E2-4621-91B4-A1A756289D20}";

        public MatchResponseTagsItem(Item innerItem)
            : base(innerItem)
        {
        }

        public static implicit operator MatchResponseTagsItem(Item innerItem)
        {
            return innerItem != null ? new MatchResponseTagsItem(innerItem) : null;
        }

        public static implicit operator Item(MatchResponseTagsItem customItem)
        {
            return customItem?.InnerItem;
        }

        public MultilistField MatchTheContentWithin => new MultilistField(InnerItem.Fields["Match the content within"]);
    }
}