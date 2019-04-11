using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Folders
{
    public class PreconditionsFolderItem : CustomItem
    {
        public const string TemplateId = "{9675D02E-D173-4760-8F13-3432B920D771}";

        public FolderItem Folder { get; }

        public BaseUrlRewriteItem BaseUrlRewriteItem { get; }

        public PreconditionsFolderItem(Item innerItem)
            : base(innerItem)
        {
            Folder = new FolderItem(innerItem);
            BaseUrlRewriteItem = new BaseUrlRewriteItem(innerItem);
        
        }

        public static implicit operator PreconditionsFolderItem(Item innerItem)
        {
            return innerItem != null ? new PreconditionsFolderItem(innerItem) : null;
        }

        public static implicit operator Item(PreconditionsFolderItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}