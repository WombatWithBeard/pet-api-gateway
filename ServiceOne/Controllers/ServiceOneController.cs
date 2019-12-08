using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceOneController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ServiceOneController> _logger;

        public ServiceOneController(ILogger<ServiceOneController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "ServiceOne";
        }
    }
}