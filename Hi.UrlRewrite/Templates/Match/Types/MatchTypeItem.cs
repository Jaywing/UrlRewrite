using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match.Types
{
    public class MatchTypeItem : CustomItem
    {
        public const string TemplateId = "{54FB682D-57DA-401F-853A-7556F0213C4C}";
        
        public MatchTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchTypeItem(Item innerItem)
        {
            return innerItem != null ? new MatchTypeItem(innerItem) : null;
        }

        public static implicit operator Item(MatchTypeItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}