using System.Web.Http;
using System.Reflection;
using QuinaNadalServer.Models;

namespace QuinaNadalServer.Controllers
{
    [RoutePrefix("api/info")]
    public class InfoController : ApiController 
    {
        [HttpGet]
        [Route("version")]
        public IHttpActionResult GetVersion()
        {
            string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Ok(Version);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetInfo()
        {
            ApiInfo apiInfo = new ApiInfo(); 

            apiInfo.NomApi = Assembly.GetExecutingAssembly().GetName().Name;
            apiInfo.VersioApi = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Ok(apiInfo);
        }
    }
}
