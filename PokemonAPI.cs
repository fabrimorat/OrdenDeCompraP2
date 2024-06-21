namespace OrdenDeCompraP2
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class PokemonAPI
    {
        private readonly HttpClient _httpClient;

        public PokemonAPI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPokemonImageUrl(string pokemonName)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"pokemon/{pokemonName}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBody);
                string imageUrl = (string)json["sprites"]["front_default"];

                return imageUrl;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }

}
