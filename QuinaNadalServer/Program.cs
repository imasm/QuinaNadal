using System;
using QuinaNadalServer.Logging;
using QuinaNadalServer.Properties;
using Topshelf;

namespace QuinaNadalServer
{
    class Program
    {
        public static int ListenPort { get; private set; }
        public static string ListenHost { get; private set; }
        public static bool DebugRequests { get; private set; }

        static void Main(string[] args)
        {
           ReadConfiguration();

            Console.WriteLine("Starting...");
            HostFactory.Run(x =>
            {
                x.Service<WebServer>(s =>
                {
                    s.ConstructUsing(name => new WebServer(ListenHost, ListenPort) { Logger = new HostServerLogger() });
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsPrompt(); // or x.RunAsLocalSystem();
                x.UseNLog();

                x.SetDescription("Quina Nadal WebApi");
                x.SetDisplayName("Quina Nadal Server");
                x.SetServiceName("QuinaNadalServer");
            });
            Console.WriteLine("Terminated");
        }

        static void ReadConfiguration()
        {
            ListenHost = Settings.Default.ListenServer;
            ListenPort = Settings.Default.ListenPort;
            DebugRequests = Settings.Default.DebugRequests;
        }
    }
}
