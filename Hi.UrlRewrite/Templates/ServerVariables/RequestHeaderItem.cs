using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Templates.ServerVariables
{
	public class RequestHeaderItem : CustomItem
	{
        public const string TemplateId = "{698FFCC4-0D22-4B97-ACFE-3D04994B4B65}";

        public BaseServerVariableItem BaseServerVariableItem { get; }

        public RequestHeaderItem(Item innerItem)
			: base(innerItem)
		{
			BaseServerVariableItem = new BaseServerVariableItem(innerItem);
		}

		public static implicit operator RequestHeaderItem(Item innerItem)
		{
			return innerItem != null ? new RequestHeaderItem(innerItem) : null;
		}

		public static implicit operator Item(RequestHeaderItem customItem)
		{
			return customItem?.InnerItem;
		}
	}
}