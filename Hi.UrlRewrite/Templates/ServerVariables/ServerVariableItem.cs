using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.ServerVariables
{
    public class ServerVariableItem : CustomItem
    {
        public const string TemplateId = "{E073CD4C-2747-433D-B6C0-3C52BA953C97}";

        public BaseServerVariableItem BaseServerVariableItem { get; }

        public ServerVariableItem(Item innerItem)
            : base(innerItem)
        {
            BaseServerVariableItem = new BaseServerVariableItem(innerItem);
        }

        public static implicit operator ServerVariableItem(Item innerItem)
        {
            return innerItem != null ? new ServerVariableItem(innerItem) : null;
        }

        public static implicit operator Item(ServerVariableItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}