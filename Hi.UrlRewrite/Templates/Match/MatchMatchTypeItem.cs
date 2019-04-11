using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchMatchTypeItem : CustomItem
    {
        public const string TemplateId = "{07597712-CC43-4AE3-BD01-01B9EB284261}";
        
        public MatchMatchTypeItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator MatchMatchTypeItem(Item innerItem)
        {
            return innerItem != null ? new MatchMatchTypeItem(innerItem) : null;
        }

        public static implicit operator Item(MatchMatchTypeItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LookupField MatchType => new LookupField(InnerItem.Fields["Match Type"]);
    }
}