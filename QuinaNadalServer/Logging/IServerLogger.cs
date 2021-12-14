using System;

namespace QuinaNadalServer.Logging
{
    public interface IServerLogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Error(string message, Exception ex);
        void Warn(string message);
    }
}