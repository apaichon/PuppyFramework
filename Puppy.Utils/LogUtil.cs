using System;
using System.IO;
using System.Text.RegularExpressions;
using Serilog;

namespace Puppy.Utils
{
    public class LogUtil: IUtil
    {
        ILogger _log;

        public LogUtil()
        {
            string logPath = this.GetApplicationRoot() + "\\logs\\myapp.txt";
            initLogger(logPath);
        }
        public LogUtil(string logPath)
        {
            initLogger(logPath);
        }

        private void initLogger(string logPath)
        {
            this._log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Async(a => a.File(logPath, rollingInterval: RollingInterval.Day, shared: true))
            .CreateLogger(); 
        }

        public void Info(string content)
        {
            this._log.Information(content);
        }

        public void Error(string content)
        {
            this._log.Error(content);
        }

        public void Warning(string content)
        {
            this._log.Warning(content);
        }

        public string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                              .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

    }
}
