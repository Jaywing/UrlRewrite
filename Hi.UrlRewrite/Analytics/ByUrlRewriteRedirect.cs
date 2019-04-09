using Sitecore.Analytics.Model;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using System;
using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Analytics
{
    internal class ByUrlRewriteRedirect : PageEventDimensionBase
    {
        private readonly Guid _redirectEventId = new Guid(Constants.RedirectEventItemId);

        public ByUrlRewriteRedirect(Guid dimensionId)
            : base(dimensionId)
        {
        }

        public override IEnumerable<string> ExtractDimensionKeys(PageEventData pageEvent)
        {
            Assert.IsNotNull(pageEvent, "pageEvent");
            Item redirectItem = Sitecore.Data.Database.GetDatabase("master").GetItem(pageEvent.ItemId.ToString());
            yield return redirectItem.Paths.ContentPath;
        }

        public override bool Filter(PageEventData pageEvent)
        {
            Assert.IsNotNull(pageEvent, "pageEvent");
            return pageEvent.PageEventDefinitionId.Equals(this._redirectEventId);
        }
    }
}