namespace Hi.UrlRewrite.Entities.Actions.Base
{
    public interface IBaseRedirect : IBaseRewriteUrl, IBaseAppendQueryString, IBaseCache, IBaseStopProcessing, IBaseStatusCode
    {
    }
}