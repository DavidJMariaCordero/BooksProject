using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BooksApi.Services.BookServices
{
    public class BookService : IBookService
    {
        protected readonly IConfiguration _configuration;
        HttpClient client = new();
        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static HttpRequestMessage GetRequestMessage(Object data, string ruta, HttpMethod accion, IConfiguration configuration)
        {
            var json = JsonConvert.SerializeObject(data);
            var url = configuration["FakeApiUrl"] + ruta;
            var request = new HttpRequestMessage
            {
                Method = accion,
                RequestUri = new Uri($"{url}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return request;
        }

        public async Task<List<Book>> GetBooks()
        {
            try
            {
                var request = GetRequestMessage(null, "api/v1/Books", HttpMethod.Get, _configuration);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadAsStringAsync();
                    var lista = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return lista.ToList();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;

        }
    }
}
