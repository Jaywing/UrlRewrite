using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Folders
{
    public class FolderItem : CustomItem
    {
        public const string TemplateId = "{A87A00B1-E6DB-45AB-8B54-636FEC3B5523}";
        
        public FolderItem(Item innerItem)
            : base(innerItem)
        {

        }

        public static implicit operator FolderItem(Item innerItem)
        {
            return innerItem != null ? new FolderItem(innerItem) : null;
        }

        public static implicit operator Item(FolderItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}