using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrdenDeCompraP2
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    namespace OrdenDeCompraP2.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PokemonController : ControllerBase
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public PokemonController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            [HttpGet("GetPokemonImage")]
            public async Task<IActionResult> GetPokemonImage(string pokemonName)
            {
                var client = _httpClientFactory.CreateClient("PokemonAPI");
                HttpResponseMessage response = await client.GetAsync($"pokemon/{pokemonName}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(content);
                    var imageUrl = (string)json["sprites"]["front_default"];
                    return Ok(new { ImageUrl = imageUrl });
                }
                return NotFound();
            }


        }
    }


}
