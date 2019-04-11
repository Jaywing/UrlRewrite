using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action.Base
{
    public class BaseRewriteUrlItem : CustomItem
    {
        public const string TemplateId = "{078D5677-484F-4998-85A6-542CE788D840}";
        
        public BaseRewriteUrlItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator BaseRewriteUrlItem(Item innerItem)
        {
            return innerItem != null ? new BaseRewriteUrlItem(innerItem) : null;
        }

        public static implicit operator Item(BaseRewriteUrlItem customItem)
        {
            return customItem?.InnerItem;
        }

        public LinkField RewriteUrl => new LinkField(InnerItem.Fields["Rewrite URL"]);
    }
}