using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchPatternItem : CustomItem
    {
        public const string TemplateId = "{E4AB5966-0E72-431B-ABB3-8CB9274CC074}";

        public MatchPatternItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchPatternItem(Item innerItem)
        {
            return innerItem != null ? new MatchPatternItem(innerItem) : null;
        }

        public static implicit operator Item(MatchPatternItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField Pattern => new TextField(InnerItem.Fields["Pattern"]);
    }
}