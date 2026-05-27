namespace Loger
{
    public class Logger
    {
        private readonly string _logFilePath;
        private readonly string _logFormat;
        private readonly IniConfig _config;

        public enum LogType
        {
            Info,
            Error,
            Warning,
            Test,
            Exception
        }

        public Logger(string configFilePath, string logFilePath)
        {
            _config = new IniConfig(configFilePath);
            _logFilePath = logFilePath;
            _logFormat = _config.GetValue("LogSettings", "Format", "{Timestamp} | {Type} | {User} | {Message}");
        }

        public void Log(LogType type, string user, string message)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = _logFormat
                .Replace("{Timestamp}", timestamp)
                .Replace("{Type}", type.ToString())
                .Replace("{User}", user)
                .Replace("{Message}", message);

            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
    }
}