using Sitecore.Pipelines.HttpRequest;
using Sitecore.Sites;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace Hi.UrlRewrite.Processing
{
    public class UrlRewriteHandler : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var urlRewriteProcessor = new InboundRewriteProcessor();
                var requestArgs = new HttpRequestArgs(new HttpContextWrapper(context), HttpRequestType.Begin);
                Uri requestUri = context.Request.Url;

                SiteContext siteContext = SiteContextFactory.GetSiteContext(requestUri.Host, requestUri.AbsolutePath,
                    requestUri.Port);

                if (siteContext != null)
                {
                    using (new SiteContextSwitcher(siteContext))
                    {
                        urlRewriteProcessor.Process(requestArgs);
                    }
                }

            }
            catch (ThreadAbortException)
            {
                // swallow this exception because we may have called Response.End
            }
            catch (HttpException)
            {
                // we want to throw http exceptions, but we don't care about logging them in Sitecore
                throw;
            }
            catch (Exception ex)
            {
                if (ex is HttpException || ex is ThreadAbortException) return;

                // log it in sitecore
                Log.Error(this, ex, "Error in UrlRewriteHandler.");

                // throw;
                // don't re-throw the error, but instead let it fall through
            }

            // if we have come this far, the url rewrite processor didn't match on anything so the request is passed to the static request handler

            // Serve static content:
            IHttpHandler staticFileHandler = null;

            try
            {
                AssemblyName systemWebAssemblyName =
                    GetType()
                        .Assembly.GetReferencedAssemblies()
                        .First(assembly => assembly.FullName.StartsWith("System.Web, "));
                Assembly systemWeb = AppDomain.CurrentDomain.Load(systemWebAssemblyName);

                Type staticFileHandlerType = systemWeb.GetType("System.Web.StaticFileHandler", true);
                staticFileHandler = Activator.CreateInstance(staticFileHandlerType, true) as IHttpHandler;

            }
            catch (Exception)
            {

            }

            if (staticFileHandler != null)
            {
                try
                {
                    staticFileHandler.ProcessRequest(context);
                }
                catch (HttpException httpException)
                {
                    if (httpException.GetHttpCode() == 404)
                    {
                        HandleNotFound(context);
                    }
                    else
                    {
                        throw;
                    }
                }    
            }
        }

        private static void HandleNotFound(HttpContext context)
        {
            if (string.IsNullOrEmpty(NotFoundPage))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
            }
            else
            {
                if (CustomErrorsRedirectMode == CustomErrorsRedirectMode.ResponseRedirect)
                {
                    context.Response.Redirect(NotFoundPage);
                }
                else
                {
                    context.Server.Transfer(NotFoundPage);
                }
            }
        }

        private static readonly CustomErrorsSection CustomErrorsSection = ConfigurationManager.GetSection("system.web/customErrors") as CustomErrorsSection;
        private static readonly CustomErrorsRedirectMode CustomErrorsRedirectMode = CustomErrorsSection.RedirectMode;
        private static readonly string NotFoundPage = GetCustomError("404");

        static protected string GetCustomError(string code)
        {
            CustomError page = CustomErrorsSection?.Errors[code];

            return page != null ? page.Redirect : CustomErrorsSection.DefaultRedirect;
        }
    }
}