using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace ViaCepWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeuCepController : ControllerBase
    {
        private readonly JsonNode data;

        public SeuCepController()
        {
            data = null;
        }

        [HttpGet(Name = "SeuCep")]
        public async Task<JsonNode>  GetCEP()
        {
            // send GET request with RestSharp
            var client = new RestClient("http://viacep.com.br/ws/");
            var request = new RestRequest("77006146/json/");
            var response = await client.ExecuteGetAsync(request);

            if (!response.IsSuccessful)
            {
                return response.ErrorException!.Message!;
            }
            // deserialize json string response to JsonNode object
            var data = JsonSerializer.Deserialize<JsonNode>(response.Content!)!;

            return data;
        }
    }
}