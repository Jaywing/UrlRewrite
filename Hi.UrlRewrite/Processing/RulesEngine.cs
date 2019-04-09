using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hi.UrlRewrite.Caching;
using Hi.UrlRewrite.Entities.Actions;
using Hi.UrlRewrite.Entities.Conditions;
using Hi.UrlRewrite.Entities.Match;
using Hi.UrlRewrite.Entities.Rules;
using Hi.UrlRewrite.Templates.Folders;
using Hi.UrlRewrite.Templates.Inbound;
using Hi.UrlRewrite.Templates.Outbound;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Hi.UrlRewrite.Processing
{
	public class RulesEngine
    {
        public Database Database { get; }

        public RulesEngine(Database db)
        {
            this.Database = db;
        }

        public List<InboundRule> GetInboundRules()
        {
            if (Database == null)
            {
                return null;
            }

            IEnumerable<Item> redirectFolderItems = GetRedirectFolderItems();

            if (redirectFolderItems == null)
            {
                return null;
            }

            var inboundRules = new List<InboundRule>();

            foreach (Item redirectFolderItem in redirectFolderItems)
            {
                Log.Debug(this, Database, "Loading Inbound Rules from RedirectFolder: {0}", redirectFolderItem.Name);

                AssembleRulesRecursive(redirectFolderItem, ref inboundRules, new RedirectFolderItem(redirectFolderItem));
            }

            return inboundRules;
        }

	    private void AssembleRulesRecursive(Item ruleOrFolderItem, ref List<InboundRule> rules, RedirectFolderItem redirectFolderItem)
	    {
			if (ruleOrFolderItem.TemplateID == new ID(new Guid(SimpleRedirectItem.TemplateId)))
			{
				var simpleRedirectItem = new SimpleRedirectItem(ruleOrFolderItem);

				Log.Debug(this, Database, "Loading SimpleRedirect: {0}", simpleRedirectItem.Name);

				InboundRule inboundRule = CreateInboundRuleFromSimpleRedirectItem(simpleRedirectItem, redirectFolderItem);

				if (inboundRule != null && inboundRule.Enabled)
				{
					rules.Add(inboundRule);
				}
			}
			else if (ruleOrFolderItem.TemplateID == new ID(new Guid(InboundRuleItem.TemplateId)))
			{
				var inboundRuleItem = new InboundRuleItem(ruleOrFolderItem);

				Log.Debug(this, Database, "Loading InboundRule: {0}", inboundRuleItem.Name);

				InboundRule inboundRule = CreateInboundRuleFromInboundRuleItem(inboundRuleItem, redirectFolderItem);

				if (inboundRule != null && inboundRule.Enabled)
				{
					rules.Add(inboundRule);
				}
			}
			else if (ruleOrFolderItem.TemplateID == new ID(new Guid(RedirectSubFolderItem.TemplateId))
			         || ruleOrFolderItem.TemplateID == new ID(new Guid(RedirectFolderItem.TemplateId)))
			{
				ChildList childRules = ruleOrFolderItem.GetChildren();
				foreach (Item childRule in childRules)
				{
					AssembleRulesRecursive(childRule, ref rules, redirectFolderItem);
				}
			}
		}

        public List<OutboundRule> GetOutboundRules()
        {
            if (Database == null)
            {
                return null;
            }

            IEnumerable<Item> redirectFolderItems = GetRedirectFolderItems();

            if (redirectFolderItems == null)
            {
                return null;
            }

            var outboundRules = new List<OutboundRule>();

            foreach (var redirectFolderItem in redirectFolderItems)
            {
                Log.Debug(this, Database, "Loading Outbound Rules from RedirectFolder: {0}", redirectFolderItem.Name);

                IEnumerable<Item> folderDescendants = redirectFolderItem.Axes.GetDescendants()
                    .Where(x => x.TemplateID == new ID(new Guid(OutboundRuleItem.TemplateId)));

                foreach (Item descendantItem in folderDescendants)
                {
                    if (descendantItem.TemplateID == new ID(new Guid(OutboundRuleItem.TemplateId)))
                    {
                        var outboundRuleItem = new OutboundRuleItem(descendantItem);

                        Log.Debug(this, Database, "Loading OutboundRule: {0}", outboundRuleItem.Name);

                        OutboundRule outboundRule = CreateOutboundRuleFromOutboundRuleItem(outboundRuleItem, redirectFolderItem);

                        if (outboundRule != null && outboundRule.Enabled)
                        {
                            outboundRules.Add(outboundRule);
                        }
                    }
                }
            }

            return outboundRules;
        }

        private IEnumerable<Item> GetRedirectFolderItems()
        {
            IEnumerable<Item> redirectFolderItems = Database.GetItem(RedirectFolderItem.TemplateId)
                .GetReferrers()
                .Where(e => e.TemplateID == new ID(RedirectFolderItem.TemplateId));

            return redirectFolderItems;
        }

        #region Serialization 
        internal InboundRule CreateInboundRuleFromSimpleRedirectItem(SimpleRedirectItem simpleRedirectItem, RedirectFolderItem redirectFolderItem)
        {
            string inboundRulePattern = $"^{simpleRedirectItem.Path.Value}/?$";
            string siteNameRestriction = GetSiteNameRestriction(redirectFolderItem);

            LinkField redirectTo = simpleRedirectItem.Target;

            GetRedirectUrlOrItemId(redirectTo, out string actionRewriteUrl, out Guid? redirectItem, out string redirectItemAnchor);

            Log.Debug(this, simpleRedirectItem.Database, "Creating Inbound Rule From Simple Redirect Item - {0} - id: {1} actionRewriteUrl: {2} redirectItem: {3}", simpleRedirectItem.Name, simpleRedirectItem.ID.Guid, actionRewriteUrl, redirectItem);

            var inboundRule = new InboundRule
            {
                Action = new Redirect
                {
                    AppendQueryString = true,
                    Name = "Redirect",
                    StatusCode = RedirectStatusCode.Permanent,
                    RewriteUrl = actionRewriteUrl,
                    RewriteItemId = redirectItem,
                    RewriteItemAnchor = redirectItemAnchor,
                    StopProcessingOfSubsequentRules = false,
                    HttpCacheability = HttpCacheability.NoCache
                },
                SiteNameRestriction = siteNameRestriction,
                Enabled = simpleRedirectItem.BaseEnabledItem.Enabled.Checked,
                IgnoreCase = true,
                ItemId = simpleRedirectItem.ID.Guid,
                ConditionLogicalGrouping = LogicalGrouping.MatchAll,
                Name = simpleRedirectItem.Name,
                Pattern = inboundRulePattern,
                MatchType = MatchType.MatchesThePattern,
                Using = Using.RegularExpressions,
				SortOrder = simpleRedirectItem.SortOrder
            };

            return inboundRule;
        }

        internal InboundRule CreateInboundRuleFromInboundRuleItem(InboundRuleItem inboundRuleItem, RedirectFolderItem redirectFolderItem)
        {
            string siteNameRestriction = GetSiteNameRestriction(redirectFolderItem);
            InboundRule inboundRule = inboundRuleItem.ToInboundRule(siteNameRestriction);

            return inboundRule;
        }

        internal OutboundRule CreateOutboundRuleFromOutboundRuleItem(OutboundRuleItem outboundRuleItem,
            RedirectFolderItem redirectFolderItem)
        {
            OutboundRule outboundRule = outboundRuleItem.ToOutboundRule();
            return outboundRule;
        }

        internal static string GetSiteNameRestriction(RedirectFolderItem redirectFolder)
        {
            string site = redirectFolder.SiteNameRestriction.Value;
            return site;
        }

        internal static void GetRedirectUrlOrItemId(LinkField redirectTo, out string actionRewriteUrl, out Guid? redirectItemId, out string redirectItemAnchor)
        {
            actionRewriteUrl = null;
            redirectItemId = null;
            redirectItemAnchor = null;

            if (redirectTo.TargetItem != null)
            {
                redirectItemId = redirectTo.TargetItem.ID.Guid;
                redirectItemAnchor = redirectTo.Anchor;
            }
            else
            {
                actionRewriteUrl = redirectTo.Url;
            }
        }

        #endregion

        #region Caching

        private RulesCache GetRulesCache()
        {
            return RulesCacheManager.GetCache(Database);
        }

        internal List<InboundRule> GetCachedInboundRules()
        {
            List<InboundRule> inboundRules = GetInboundRules();

            if (inboundRules != null)
            {
                Log.Info(this, Database, "Adding {0} rules to Cache [{1}]", inboundRules.Count, Database.Name);

                RulesCache cache = GetRulesCache();
                cache.SetInboundRules(inboundRules);
            }
            else
            {
                Log.Info(this, Database, "Found no rules");
            }

            return inboundRules;
        }

        internal List<OutboundRule> GetCachedOutboundRules()
        {
            List<OutboundRule> outboundRules = GetOutboundRules();

            if (outboundRules != null)
            {
                Log.Info(this, Database, "Adding {0} rules to Cache [{1}]", outboundRules.Count, Database.Name);

                RulesCache cache = GetRulesCache();
                cache.SetOutboundRules(outboundRules);
            }
            else
            {
                Log.Info(this, Database, "Found no rules");
            }

            return outboundRules;
        }

		/// <summary>
		/// Checks to see whether this individual inbound rule can be updated without refreshing all rules
		/// </summary>
	    internal bool CanRefreshInboundRule(Item item, Item redirectFolderItem)
	    {
		    var result = false;

			RulesCache cache = GetRulesCache();
		    List<InboundRule> inboundRules = cache.GetInboundRules();

		    if (inboundRules != null)
		    {
				// It's only possible to update this individual rule if its position in the tree has not changed. Sort order is an imperfect test, but it will catch most cases.
			    result = inboundRules.Any(r => r.ItemId == item.ID.Guid && r.SortOrder == item.Appearance.Sortorder);
			}

		    return result;
	    }


		internal void RefreshRule(Item item, Item redirectFolderItem)
        {
            UpdateRulesCache(item, redirectFolderItem, AddRule);
        }

        internal void DeleteRule(Item item, Item redirectFolderItem)
        {
            UpdateRulesCache(item, redirectFolderItem, RemoveRule);
        }

        private void UpdateRulesCache(Item item, Item redirectFolderItem, Action<Item, Item, List<IBaseRule>> action)
        {
            RulesCache cache = GetRulesCache();
            IEnumerable<IBaseRule> baseRules = null;
            if (item.IsSimpleRedirectItem() || item.IsInboundRuleItem())
            {
                baseRules = cache.GetInboundRules() ?? GetInboundRules();
            }
            else if (item.IsOutboundRuleItem())
            {
                baseRules = cache.GetOutboundRules() ?? GetOutboundRules();
            }

            if (baseRules != null)
            {
                List<IBaseRule> rules = baseRules.ToList();

                action(item, redirectFolderItem, rules);

                Log.Debug(this, item.Database, "Updating Rules Cache - count: {0}", rules.Count());

                // update the cache
                if (item.IsSimpleRedirectItem() || item.IsInboundRuleItem())
                {
                    cache.SetInboundRules(rules.OfType<InboundRule>());
                }
                else if (item.IsOutboundRuleItem())
                {
                    cache.SetOutboundRules(rules.OfType<OutboundRule>());
                }
            }
        }

        private void AddRule(Item item, Item redirectFolderItem, List<IBaseRule> inboundRules)
        {
            IBaseRule newRule = null;

            Log.Debug(this, item.Database, "Adding Rule - item: [{0}]", item.Paths.FullPath);

            if (item.IsInboundRuleItem())
            {
                newRule = CreateInboundRuleFromInboundRuleItem(item, redirectFolderItem);
            }
            else if (item.IsSimpleRedirectItem())
            {
                newRule = CreateInboundRuleFromSimpleRedirectItem(item, redirectFolderItem);
            }
            else if (item.IsOutboundRuleItem())
            {
                newRule = CreateOutboundRuleFromOutboundRuleItem(item, redirectFolderItem);
            }

            if (newRule != null)
            {
                AddOrRemoveRule(item, redirectFolderItem, inboundRules, newRule);
            }
        }

        private void AddOrRemoveRule(Item item, Item redirectFolderItem, List<IBaseRule> rules, IBaseRule newRule)
        {
            if (newRule.Enabled)
            {
                IBaseRule existingRule = rules.FirstOrDefault(e => e.ItemId == item.ID.Guid);
                if (existingRule != null)
                {

                    Log.Debug(this, item.Database, "Replacing Rule - item: [{0}]", item.Paths.FullPath);

                    int index = rules.FindIndex(e => e.ItemId == existingRule.ItemId);
                    rules.RemoveAt(index);
                    rules.Insert(index, newRule);
                }
                else
                {
                    Log.Debug(this, item.Database, "Adding Rule - item: [{0}]", item.Paths.FullPath);

                    rules.Add(newRule);
                }
            }
            else
            {
                RemoveRule(item, redirectFolderItem, rules);
            }
        }

        private void RemoveRule(Item item, Item redirectFolderItem, List<IBaseRule> inboundRules)
        {
            Log.Debug(this, item.Database, "Removing Rule - item: [{0}]", item.Paths.FullPath);

            IBaseRule existingInboundRule = inboundRules.FirstOrDefault(e => e.ItemId == item.ID.Guid);
            if (existingInboundRule != null)
            {
                inboundRules.Remove(existingInboundRule);
            }
		}

		/// <summary>
		/// Clears the cache of inbound rules
		/// </summary>
		internal void ClearInboundRuleCache()
		{
			RulesCache cache = GetRulesCache();
			cache.ClearInboundRules();
		}

		/// <summary>
		/// Clears the cache of outbound rules
		/// </summary>
		internal void ClearOutboundRuleCache()
		{
			RulesCache cache = GetRulesCache();
			cache.ClearOutboundRules();
		}
		#endregion
	}
}