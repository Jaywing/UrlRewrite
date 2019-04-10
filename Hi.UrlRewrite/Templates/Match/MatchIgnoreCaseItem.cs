using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchIgnoreCaseItem : CustomItem
    {
        public const string TemplateId = "{E4AB5966-0E72-431B-ABB3-8CB9274CC074}";

        public MatchIgnoreCaseItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchIgnoreCaseItem(Item innerItem)
        {
            return innerItem != null ? new MatchIgnoreCaseItem(innerItem) : null;
        }

        public static implicit operator Item(MatchIgnoreCaseItem customItem)
        {
            return customItem?.InnerItem;
        }

        public CheckboxField IgnoreCase => new CheckboxField(InnerItem.Fields["Ignore case"]);
    }
}