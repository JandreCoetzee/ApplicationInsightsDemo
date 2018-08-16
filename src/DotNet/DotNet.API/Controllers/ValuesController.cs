using System;
using System.Web.Http;

namespace DotNet.API.Controllers
{
    [RoutePrefix("api/v1")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("divide-handled")]
        public IHttpActionResult DivideHandled()
        {
            try
            {
                var result = this.Divide(10, 0);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("divide-unhandled")]
        public IHttpActionResult DivideUnhandled()
        {
            var result = this.Divide(10, 0);
            return Ok(result);
        }

        private int Divide(int numerator, int denominator)
        {
            return numerator / denominator;
        }
    }
}
