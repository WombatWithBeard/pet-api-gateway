using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceTwoController : ControllerBase
    {
        private readonly ILogger<ServiceTwoController> _logger;

        public ServiceTwoController(ILogger<ServiceTwoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "ServiceTwo";
        }
    }
}