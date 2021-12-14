using System;
using Topshelf.Logging;

namespace QuinaNadalServer.Logging
{
    internal class HostServerLogger : IServerLogger
    {
        private const string LoggerName = "HostServer";

        public void Debug(string message)
        {
            LogWriter log = HostLogger.Get(LoggerName);
            log.Debug(message);
        }

        public void Info(string message)
        {
            LogWriter log = HostLogger.Get(LoggerName);
            log.Info(message);
        }

        public void Warn(string message)
        {
            LogWriter log = HostLogger.Get(LoggerName);
            log.Warn(message);
        }

        public void Error(string message)
        {
            LogWriter log = HostLogger.Get(LoggerName);
            log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            LogWriter log = HostLogger.Get(LoggerName);
            log.Error(message, ex);
        }
    }
}