using System;
using System.Web.Http;

namespace DotNet.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("values")]
        public IHttpActionResult Get()
        {
            try
            {
                this.GetValue();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string GetValue()
        {
            throw new NotImplementedException("This endpoint has not yet been implemented.");
        }
    }
}
