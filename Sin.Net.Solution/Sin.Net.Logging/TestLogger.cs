using NLog;
using NLog.Layouts;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sin.Net.Logging
{
    public class TestLogger : ILoggable
    {
        // -- fields

        private Logger _logger;

        // -- constructor

        public TestLogger()
        {
            Start();
        }

        // -- methods

        #region Start
        public ILoggable Start()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var consoleTarget = new NLog.Targets.ConsoleTarget("logconsole");

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

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget);

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

        public void Trace(object data)
        {
            Trace(Convert(data));
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Debug(object data)
        {
            Debug(Convert(data));
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Info(object data)
        {
            Info(Convert(data));
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
        }

        public void Warn(object data)
        {
            Warn(Convert(data));
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Error(object data)
        {
            Error(Convert(data));
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex);
        }

        public string Convert(object attachment)
        {
            var a = string.Empty;

            if (attachment == null)
            {
                a = "null";
            }
            else if (attachment is IList &&
                     attachment.GetType().IsGenericType &&
                     attachment.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
            {
                var list = attachment as IList;
                a = "A list:";
                if (list.Count > 0 && list[0] != null)
                {
                    a += $" of type {list[0].GetType().Name}";
                }
                a += $" with {list.Count} {(list.Count == 1 ? "element" : "elements")}";
            }
            else
            {
                a = $"{attachment.GetType().Name}: {attachment.ToString()}";
            }

            return a;
        }
    }
}
