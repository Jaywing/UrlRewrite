using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Folders
{
    public class RedirectSubFolderItem : CustomItem
    {
        public const string TemplateId = "{9461E537-8E89-4B91-896A-1F2C3AF4A3D5}";

        public FolderItem FolderItem { get; }

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public RedirectSubFolderItem(Item innerItem)
            : base(innerItem)
        {
            FolderItem = new FolderItem(innerItem);
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
        
        }

        public static implicit operator RedirectSubFolderItem(Item innerItem)
        {
            return innerItem != null ? new RedirectSubFolderItem(innerItem) : null;
        }

        public static implicit operator Item(RedirectSubFolderItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}