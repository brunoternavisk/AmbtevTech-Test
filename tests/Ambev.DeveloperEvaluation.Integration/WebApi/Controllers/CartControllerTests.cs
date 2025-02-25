using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class CartControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CartControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostCart_Should_Return_Created()
    {
        // 🔹 Criando um carrinho fictício com produtos válidos
        var cart = new
        {
            userId = 321,  // ID de usuário fictício
            products = new[]
            {
                new { id = 0, cartId = 1, productId = 700, quantity = 5, unitPrice = 20.0m }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(cart), Encoding.UTF8, "application/json");

        // ✅ Chamando a API
        var response = await _client.PostAsync("/api/Cart", content);
        
        // ✅ Verificando o status esperado
        Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

        // ✅ Lendo o corpo da resposta e verificando se contém um ID válido
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseJson = JsonSerializer.Deserialize<JsonElement>(responseBody);

        Assert.True(responseJson.TryGetProperty("id", out var idProperty), "A resposta deve conter um ID.");
        Assert.True(idProperty.GetInt32() > 0, "O ID retornado deve ser um número positivo.");
    }
}