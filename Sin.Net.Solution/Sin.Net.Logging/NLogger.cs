using NLog;
using NLog.Layouts;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.IO;

namespace Sin.Net.Logging
{
    public class NLogger : ILoggable
    {
        // -- fields

        private Logger _logger;


        // -- constructors

        public NLogger()
        {
            Suffix = string.Empty;
            DeleteOlFiles = true;
            MinRule = LogLevel.Info;
        }

        // -- methods

        #region Start
        public ILoggable Start()
        {
            var path = "Logs/";
            var config = new NLog.Config.LoggingConfiguration();
            var consoleTarget = new NLog.Targets.ConsoleTarget("logconsole");
            var fileTarget = new NLog.Targets.FileTarget("logfile");
            var filename = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            consoleTarget.Layout = new CsvLayout()
            {
                Columns = {
                    new CsvColumn("Time", "${date:format=yyyy-MM-dd HH\\:mm\\:ss}"),
                    new CsvColumn("Level", "${level}"),
                    new CsvColumn("Message", "${message}"),
                },
                Delimiter = CsvColumnDelimiterMode.Custom,
                CustomColumnDelimiter = " | ",
                Quoting = CsvQuotingMode.Nothing,
            };

            fileTarget.Layout = new CsvLayout()
            {
                Columns = {
                    new CsvColumn("Time", "${date:format=yyyy-MM-dd HH\\:mm\\:ss}"),
                    new CsvColumn("Level", "${level}"),
                    new CsvColumn("Message", "${message}"),
                },
                Delimiter = CsvColumnDelimiterMode.Tab
            };

            fileTarget.FileName = $"{Path.Combine(path, filename)}{Suffix}.log";
            fileTarget.DeleteOldFileOnStartup = DeleteOlFiles;
            config.AddRule(MinRule, LogLevel.Fatal, consoleTarget);
            config.AddRule(MinRule, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
            _logger = LogManager.GetCurrentClassLogger();

            return this;
        }
        #endregion

        public void Stop()
        {
            NLog.LogManager.Shutdown();
        }

        // -- log methods

        public void Trace(string msg)
        {
            _logger.Trace(msg);
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex);
        }

        // properties

        public LogLevel MinRule { get; set; }

        public bool DeleteOlFiles { get; set; }

        public string Suffix { get; set; }

    }
}