using System;
using NLog;

namespace QuinaNadalServer.Logging
{
    internal class NLogServerLogger : IServerLogger
    {
        private readonly Logger _log = LogManager.GetLogger("Server");

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(ex, message);
        }
    }
}