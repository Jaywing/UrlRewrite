using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Folders
{
    public class RedirectFolderItem : CustomItem
    {
        public const string TemplateId = "{CBE995D0-FCE0-4061-B807-B4BBC89962A7}";

        public FolderItem Folder { get; }

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public RedirectFolderItem(Item innerItem)
            : base(innerItem)
        {
            Folder = new FolderItem(innerItem);
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
        
        }

        public static implicit operator RedirectFolderItem(Item innerItem)
        {
            return innerItem != null ? new RedirectFolderItem(innerItem) : null;
        }

        public static implicit operator Item(RedirectFolderItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField SiteNameRestriction => new TextField(InnerItem.Fields["Site Name Restriction"]);
    }
}