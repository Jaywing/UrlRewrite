using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Inbound
{
	public class SimpleRedirectItem : CustomItem
    {
        public const string TemplateId = "{E30B15B9-34CD-419C-8671-60FEAAAD5A46}";

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public BaseEnabledItem BaseEnabledItem { get; }

        public SimpleRedirectItem(Item innerItem)
            : base(innerItem)
        {
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
            BaseEnabledItem = new BaseEnabledItem(innerItem);
        }

        public static implicit operator SimpleRedirectItem(Item innerItem)
        {
            return innerItem != null ? new SimpleRedirectItem(innerItem) : null;
        }

        public static implicit operator Item(SimpleRedirectItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField Path => new TextField(InnerItem.Fields["Path"]);

        public LinkField Target => new LinkField(InnerItem.Fields["Target"]);

        public int SortOrder => this.InnerItem.Appearance.Sortorder;
    }
}