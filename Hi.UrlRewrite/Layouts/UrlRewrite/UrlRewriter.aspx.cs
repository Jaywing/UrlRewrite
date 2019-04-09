using System;
using System.Web;
using Hi.UrlRewrite.Processing;
using Hi.UrlRewrite.Processing.Results;
using Sitecore.Data;
using Sitecore.SecurityModel;

namespace Hi.UrlRewrite.Layouts.UrlRewrite
{
    public partial class UrlRewriter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (new SecurityDisabler())
            {
                var urlRewriter = new InboundRewriter(Request.ServerVariables, Request.Headers);

                Database db = Sitecore.Data.Database.GetDatabase(Context.Items["urlrewrite:db"] as string);

                if (Context.Items["urlrewrite:result"] is ProcessInboundRulesResult requestResult)
                {
                    //TODO: Currently this only reflects the result of Redirect Actions - make this call to logging reflect all action types
                    Log.Info(this, db, "Redirecting {0} to {1} [{2}]", requestResult.OriginalUri, requestResult.RewrittenUri, requestResult.StatusCode);

                    urlRewriter.ExecuteResult(new HttpContextWrapper(Context), requestResult);
                }
            }
        }

    }
}