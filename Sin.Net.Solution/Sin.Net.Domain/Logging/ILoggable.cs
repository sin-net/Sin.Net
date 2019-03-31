using System;

namespace Sin.Net.Domain.Logging
{
    public interface ILoggable
    {
        void Start();
        void Stop();
        void Trace(string msg);
        void Debug(string msg);
        void Info(string msg);
        void Warn(string msg);
        void Error(string msg);
        void Fatal(Exception ex);
    }
}
