using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrdenDeCompraP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("APIFacturacionClient");
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            // Generar CSV sin imagen
            var csvContent = $"OrderId,PokemonName,Quantity,CustomerInfo\n";
            csvContent += $"1,{order.PokemonName},{order.Quantity},{order.CustomerInfo}\n";
            var byteArray = Encoding.UTF8.GetBytes(csvContent);
            var csvResult = new FileContentResult(byteArray, "text/csv")
            {
                FileDownloadName = "Order.csv"
            };

            // Enviar la orden a la API de facturación sin imagen
            var orderJson = JsonSerializer.Serialize(order);
            var content = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7043/api/orders", content);

            if (response.IsSuccessStatusCode)
            {
                // Devolver el CSV
                return csvResult;
            }
            else
            {
                return BadRequest("No se pudo enviar la orden al sistema de facturación");
            }
        }
    }
}
