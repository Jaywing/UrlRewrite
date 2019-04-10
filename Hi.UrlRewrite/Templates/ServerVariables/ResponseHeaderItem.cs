using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.ServerVariables
{
    public class ResponseHeaderItem : CustomItem
    {
        public const string TemplateId = "{BA67D115-9A49-4EAB-8CDF-596CB6F0CBF1}";

        public BaseServerVariableItem BaseServerVariableItem { get; }

        public ResponseHeaderItem(Item innerItem)
            : base(innerItem)
        {
            BaseServerVariableItem = new BaseServerVariableItem(innerItem);
        }

        public static implicit operator ResponseHeaderItem(Item innerItem)
        {
            return innerItem != null ? new ResponseHeaderItem(innerItem) : null;
        }

        public static implicit operator Item(ResponseHeaderItem customItem)
        {
            return customItem?.InnerItem;
        }
    }
}