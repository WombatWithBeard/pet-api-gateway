using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceOneController : ControllerBase
    {
        private readonly ILogger<ServiceOneController> _logger;

        public ServiceOneController(ILogger<ServiceOneController> logger)
        {
            _logger = logger;
        }
        
        [Route("/secret")]
        [HttpGet]
        [Authorize]
        public string Secret()
        {
            return "Message from ApiOne";
        }

        public string Get()
        {
            return "ServiceOne";
        }
    }
}