using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Match
{
    public class MatchTagItem : CustomItem
    {
        public const string TemplateId = "{B88D1713-7511-40D0-B71D-51A5E14C7C7E}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public MatchTagItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
        }

        public static implicit operator MatchTagItem(Item innerItem)
        {
            return innerItem != null ? new MatchTagItem(innerItem) : null;
        }

        public static implicit operator Item(MatchTagItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField Tag => new TextField(InnerItem.Fields["Tag"]);

        public TextField Attribute => new TextField(InnerItem.Fields["Attribute"]);
    }
}