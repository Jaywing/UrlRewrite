using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class BaseMatchItem : CustomItem
    {
        public const string TemplateId = "{57516483-A64C-4036-895B-B55D9267A8E6}";

        public MatchIgnoreCaseItem MatchIgnoreCaseItem { get; }

        public MatchMatchTypeItem MatchMatchTypeItem { get; }

        public MatchPatternItem MatchPatternItem { get; }

        public MatchUsingItem MatchUsingItem { get; }

        public BaseMatchItem(Item innerItem)
            : base(innerItem)
        {
            MatchIgnoreCaseItem = new MatchIgnoreCaseItem(innerItem);
            MatchMatchTypeItem = new MatchMatchTypeItem(innerItem);
            MatchPatternItem = new MatchPatternItem(innerItem);
            MatchUsingItem = new MatchUsingItem(innerItem);
        }

        public static implicit operator BaseMatchItem(Item innerItem)
        {
            return innerItem != null ? new BaseMatchItem(innerItem) : null;
        }

        public static implicit operator Item(BaseMatchItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}