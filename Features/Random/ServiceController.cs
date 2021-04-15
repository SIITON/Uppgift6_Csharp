using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uppgift6_Csharp.Features.Random
{
    [ApiController]
    [Route("random")]
    public class ServiceController : Controller
    {
        private readonly ISomeService _someService;
        public ServiceController(ISomeService someService)
        {
            _someService = someService;
        }
        [HttpGet]
        public ActionResult<Weather> Get()
        {
            return Ok(_someService.GetData());
        }
    }
}
