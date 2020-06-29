using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
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

        [HttpGet("/Test")]
        public async Task<IActionResult> Test()
        {
            var serverClient = _httpClientFactory.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:6661/");

            var tokenResponse =
                await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "ServiceTwo",
                    ClientSecret = "TwoSecret",
                    Scope = "ServiceOneApi"
                });

            //Get data from ApiOne
            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:4001/secret");

            var content = await response.Content.ReadAsStringAsync();
            
            return Ok(new
            {
                access_token = tokenResponse.AccessToken,
                message = content
            });

        }

        public string Get()
        {
            return "ServiceTwo";
        }
    }
}