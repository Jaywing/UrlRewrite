using Hi.UrlRewrite.Templates.Action.Base;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.Action
{
    public class CustomResponseItem : CustomItem
    {
        public const string TemplateId = "{A96D8E98-38B7-4D99-BE55-D5225F6D8279}";

        public BaseActionItem BaseActionItem { get; }

        public CustomResponseItem(Item innerItem)
            : base(innerItem)
        {
            BaseActionItem = new BaseActionItem(innerItem);
        }

        public static implicit operator CustomResponseItem(Item innerItem)
        {
            return innerItem != null ? new CustomResponseItem(innerItem) : null;
        }

        public static implicit operator Item(CustomResponseItem customItem)
        {
            return customItem?.InnerItem;
        }

        public TextField StatusCode => new TextField(InnerItem.Fields["Status code"]);

        public TextField SubStatusCode => new TextField(InnerItem.Fields["Substatus code"]);

        public TextField Reason => new TextField(InnerItem.Fields["Reason"]);

        public TextField ErrorDescription => new TextField(InnerItem.Fields["Error description"]);
    }
}