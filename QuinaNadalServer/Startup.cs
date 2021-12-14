
using System.Web.Http;
using Owin;
using QuinaNadalServer.Logging;
using QuinaNadalServer.Services;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.WebApi;

namespace QuinaNadalServer
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Default to json
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.SetupUnityContainer();

            if (Program.DebugRequests)
                config.AddLogHandlers();

            app.UseWebApi(config);
        }
    }

    static class HttpConfigurationExtensions
    {
        private static TaulellService _taulellService;

        public static void AddLogHandlers(this HttpConfiguration config)
        {
            var handler = new LogRequestAndResponseHandler();
            config.MessageHandlers.Add(handler);
        }

        public static void SetupUnityContainer(this HttpConfiguration config)
        {
            var container = new UnityContainer();

            _taulellService = new TaulellService();
            container.RegisterInstance<ITaulellService>(_taulellService);
            //container.RegisterType<ITaulellService, TaulellService>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityDependencyResolver(container);
        }

    
    }



}
