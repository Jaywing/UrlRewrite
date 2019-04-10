using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class RewriteItem : CustomItem
    {
        public const string TemplateId = "{7ACB0905-11D7-47BD-94B3-A903D264135F}";

        public BaseRewriteItem BaseRewriteItem { get; }

        public RewriteItem(Item innerItem)
            : base(innerItem)
        {
            BaseRewriteItem = new BaseRewriteItem(innerItem);
        }

        public static implicit operator RewriteItem(Item innerItem)
        {
            return innerItem != null ? new RewriteItem(innerItem) : null;
        }

        public static implicit operator Item(RewriteItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField LogRewrittenUrl => new TextField(InnerItem.Fields["Log rewritten url"]);
    }
}