using Hi.UrlRewrite.Processing;
using System;
using System.Web;

namespace Hi.UrlRewrite.Module
{
    public class OutboundModule : IHttpModule
    {
        public void Dispose()
        {
        }
         
        public void Init(HttpApplication context)
        {
            context.ReleaseRequestState += Context_ReleaseRequestState;
        }

        private static void Context_ReleaseRequestState(object sender, EventArgs e)
        {
            if (!(sender is HttpApplication app)) return;

            var context = new HttpContextWrapper(app.Context);
            var outboundRewriteProcessor = new OutboundRewriteProcessor();
            outboundRewriteProcessor.Process(context);
        }

    }
}