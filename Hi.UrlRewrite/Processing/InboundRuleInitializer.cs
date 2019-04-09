using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Pipelines;
using Sitecore.SecurityModel;
using System;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Workflows;

namespace Hi.UrlRewrite.Processing
{
    public class InboundRuleInitializer
    {
        private readonly string _masterDatabaseName = "master";

        public void Process(PipelineArgs args)
        {
            Log.Info(this, "Initializing URL Rewrite.");

            try
            {
                using (new SecurityDisabler())
                {
                    // cache all of the rules
                    foreach (Database db in Factory.GetDatabases().Where(e => e.HasContentItem))
                    {
                        var rulesEngine = new RulesEngine(db);
                        rulesEngine.GetCachedInboundRules();
                    }

                    // make sure that the page event has been deployed
                    DeployEventIfNecessary();
                }
            }
            catch (Exception ex)
            {
                Log.Error(this, ex, "Exception during initialization.");
            }
        }

        private void DeployEventIfNecessary()
        {
            if (!Factory.GetDatabaseNames().Contains(_masterDatabaseName, StringComparer.InvariantCultureIgnoreCase))
            {
                Log.Info(this, "Skipping DeployEventIfNecessary() as '{0}' database not present", _masterDatabaseName);
                return;
            }

            Database database = Sitecore.Data.Database.GetDatabase(_masterDatabaseName);

            Item eventItem = database?.GetItem(new ID(Constants.RedirectEventItemId));
            if (eventItem == null)
            {
                return;
            }

            IWorkflow workflow = database.WorkflowProvider.GetWorkflow(eventItem);

            WorkflowState workflowState = workflow?.GetState(eventItem);
            if (workflowState == null)
            {
                return;
            }

            const string analyticsDraftStateWorkflowId = "{39156DC0-21C6-4F64-B641-31E85C8F5DFE}";

            if (!workflowState.StateID.Equals(analyticsDraftStateWorkflowId))
            {
                return;
            }

            const string analyticsDeployCommandId = "{4044A9C4-B583-4B57-B5FF-2791CB0351DF}";
            WorkflowResult workflowResult = workflow.Execute(analyticsDeployCommandId, eventItem, "Deploying UrlRewrite Redirect event during initialization", false, new object[0]);
        }
    }
}