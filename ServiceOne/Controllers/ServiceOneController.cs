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

        [Route("/[controller]/secret")]
        [HttpGet]
        [Authorize]
        public string Secret()
        {
            return "Secret message from ApiOne";
        }
        
        [HttpGet]
        public string Get()
        {
            return "ServiceOne";
        }
    }
}