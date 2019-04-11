using Hi.UrlRewrite.Processing.Results;
using Sitecore.Analytics;
using Sitecore.Data;
using System;
using System.Linq;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Analytics
{
    public class Tracking
    {
        private static readonly Guid RedirectEventItemId = new Guid(Constants.RedirectEventItemId);
        private static readonly Tracking tracking = new Tracking();

        public static void TrackRedirect(ProcessInboundRulesResult results)
        {
            tracking.RegisterEventOnRedirect(results);
        }

        private void RegisterEventOnRedirect(ProcessInboundRulesResult results)
        {
            if (!Tracker.Enabled)
                return;

            if (!Tracker.IsActive)
                Tracker.StartTracking();

            try
            {
                foreach (InboundRuleResult result in results.ProcessedResults.Where(e => e.RuleMatched))
                {
                    Guid itemId = result.ItemId;
                    Item redirectItem = Sitecore.Context.Database.GetItem(new ID(itemId));

                    if (redirectItem != null)
                    {
                        var pageEventModel = new Sitecore.Analytics.Model.PageEventData()
                        {
                            PageEventDefinitionId = RedirectEventItemId,
                            ItemId = itemId,
                            Name = "UrlRewrite Redirect",
                            DateTime = DateTime.UtcNow,
                            Text = $"Redirected from {result.OriginalUri} to {result.RewrittenUri} using {redirectItem.Name} [{itemId}]."
                        };

                        var pageEventData = new Sitecore.Analytics.Data.PageEventData(pageEventModel);

                        Tracker.Current.CurrentPage.Item = new Sitecore.Analytics.Model.ItemData
                        {
                            Id = itemId,
                            Language = redirectItem.Language.Name,
                            Version = redirectItem.Version.Number
                        };

                        Tracker.Current.CurrentPage.Register(pageEventData);
                        Tracker.Current.Interaction.AcceptModifications();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex, "Exception occurred during tracking.");
            }
        }
    }
}