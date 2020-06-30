using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceTwo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceTwoController : ControllerBase
    {
        private readonly ILogger<ServiceTwoController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ServiceTwoController(ILogger<ServiceTwoController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Type()
        {
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:4501/");

            var tokenResponse =
                await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "ApiOne"
                });

            //Get data from ApiOne
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // var response = await apiClient.GetAsync("https://localhost:3001/secret");
            // var response15 = await apiClient.GetAsync("https://localhost:3001/serviceone/secret");
            // var response2 = await apiClient.GetAsync("https://localhost:5001/serviceone/serviceone/");
            var response3 = await apiClient.GetAsync("https://localhost:5001/serviceone/serviceone/secret");

            // var apiClient2 = _httpClientFactory.CreateClient();
            // var response32 = await apiClient2.GetAsync("https://localhost:5001/serviceone/serviceone/secret");
            
            var content = await response3.Content.ReadAsStringAsync();
            
            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });
        }
        
        [HttpGet]
        public string Get()
        {
            return "ServiceTwo";
        }
    }
}