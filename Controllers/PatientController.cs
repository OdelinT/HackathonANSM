using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        // POST api/values
        [HttpPost("{id}")]
        public ActionResult<string> Post(string id)
        {
            return id;
        }
    }
}
