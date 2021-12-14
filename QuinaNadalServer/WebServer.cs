using System;
using QuinaNadalServer.Logging;
using Microsoft.Owin.Hosting;

namespace QuinaNadalServer
{
    internal class WebServer : IDisposable
    {
        private IDisposable _webapp;
        public IServerLogger Logger { get; set; }

        public string ListenHost { get; }
        public int ListenPort { get; }

        public WebServer(string listenHost, int listenPort)
        {
            ListenHost = listenHost;
            ListenPort = listenPort;
            Logger = null;
        }

        public void Start()
        {
            string listenerUrl = $"{ListenHost}:{ListenPort}";
            try
            {
                _webapp = WebApp.Start<Startup>(listenerUrl);
                LogInfo($"Service is running on {listenerUrl}.");
            } catch (Exception ex)
            {
                LogError("Server not started", ex);
            }
        }        

        public void Stop()
        {
            if (_webapp != null)
            {
                LogInfo("Server is stopping...");
                _webapp.Dispose();
                _webapp = null;
                LogInfo("Server stopped.");
            }
        }

        private void LogInfo(string message)
        {
            Logger?.Info(message);
        }

        private void LogError(string message, Exception ex)
        {
            Logger?.Error(message, ex);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            Stop();
        }

        #endregion
    }
}
