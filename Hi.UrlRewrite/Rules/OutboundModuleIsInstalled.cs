using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System.Linq;
using System.Web;
using Hi.UrlRewrite.Module;

namespace Hi.UrlRewrite.Rules
{
    public class OutboundModuleIsInstalled<T> : WhenCondition<T> where T:RuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            return Check();
        }

        private static bool? isInstalled;

        private static bool Check()
        {
            var check = false;

            if (isInstalled.HasValue)
            {
                check = isInstalled.Value;
            }
            else if (HttpContext.Current != null)
            {
                HttpModuleCollection modules = HttpContext.Current.ApplicationInstance.Modules;

                isInstalled = check = modules.Cast<string>().Any(key => modules[key] is OutboundModule);
            }

            return check;
        }
    }
}