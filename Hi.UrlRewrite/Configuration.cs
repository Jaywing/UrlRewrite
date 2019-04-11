using Sitecore.Configuration;

namespace Hi.UrlRewrite
{
    public static class Configuration
    {
        public static string[] IgnoreUrlPrefixes => Settings.GetSetting("Hi.UrlRewrite.IgnoreUrlPrefixes", "/sitecore").Split('|');

        public static string CacheSize => Settings.GetSetting("Hi.UrlRewrite.CacheSize", "10MB");

        public static bool LogFileEnabled => Settings.GetBoolSetting("Hi.UrlRewrite.LogFileEnabled", false);

        public static string LogFileName => Settings.GetSetting("Hi.UrlRewrite.LogFileName", @"$(dataFolder)/logs/UrlRewrite.log.{date}.txt");

        public static string LogFileLevel => Settings.GetSetting("Hi.UrlRewrite.LogFileLevel", "INFO");

        public static bool AnalyticsTrackingEnabled => Settings.GetBoolSetting("Hi.UrlRewrite.AnalyticsTrackingEnabled", true);
    }
}