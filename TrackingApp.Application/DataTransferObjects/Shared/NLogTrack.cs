using NLog;

namespace TrackingApp.Application.DataTransferObjects.Shared
{
    public class NLogTrack
    {
        public Logger logger { get; set; }
        public string TraceIdentifier { get; set; } = "Undefined";
        public string CookieUserId { get; set; } = "Undefined";
        public string GetLogMessage(string message) => $"TraceId={TraceIdentifier}|CookieUserId={CookieUserId}|{message}";
        public void Info(string message) => logger.Info($"TraceId={TraceIdentifier}|CookieUserId={CookieUserId}|{message}");
        public void Warn(string message) => logger.Warn($"TraceId={TraceIdentifier}|CookieUserId={CookieUserId}|{message}");
        public void Error(string message) => logger.Error($"TraceId={TraceIdentifier}|CookieUserId={CookieUserId}|{message}");

        private static readonly Logger activityLogger = LogManager.GetLogger("ActivityLogs");
        private static readonly Logger accessLogger = LogManager.GetLogger("AccessLogs");
        public void LogActivity(string message)
        {
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Info, activityLogger.Name, message);
            logEventInfo.Properties["TraceIdentifier"] = TraceIdentifier;
            logEventInfo.Properties["CookieUserId"] = CookieUserId;
            activityLogger.Log(logEventInfo);
        }

        public void LogAccess(string message)
        {
            LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Info, accessLogger.Name, message);
            logEventInfo.Properties["TraceIdentifier"] = TraceIdentifier;
            logEventInfo.Properties["CookieUserId"] = CookieUserId;
            accessLogger.Log(logEventInfo);
        }
    }
}
