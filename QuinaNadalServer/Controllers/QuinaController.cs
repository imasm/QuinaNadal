using QuinaNadalServer.Models;
using QuinaNadalServer.Services;
using System.Web.Http;

namespace QuinaNadalServer.Controllers
{
    [RoutePrefix("api/quina")]
    public class QuinaController : ApiController
    {
        private readonly ITaulellService _taulellService;

        public QuinaController(ITaulellService taulellService)
        {
            _taulellService = taulellService;
        }

        [HttpGet]
        [Route("{key}")]
        public IHttpActionResult Get(string key)
        {
            if (!string.Equals(key, Keys.KeyGet))
                return BadRequest();

            Taulell result = _taulellService.GetTaulell();
            return Ok(result);
        }

        [HttpPut]
        [Route("{key}")]
        public IHttpActionResult Get(string key, [FromBody] Taulell taulell)
        {
            if (!string.Equals(key, Keys.KeySet))
                return BadRequest();

            _taulellService.SetTaulell(taulell);
            return Ok();
        }
    }
}