using System;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                return this.GetValue();
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
