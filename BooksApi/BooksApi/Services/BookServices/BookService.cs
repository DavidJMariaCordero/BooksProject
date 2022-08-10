using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BooksApi.BookServices
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

        public async Task<HttpResponseMessage> GetBooks()
        {
            var request = GetRequestMessage(null, "api/v1/Books", HttpMethod.Get, _configuration);
            return await client.SendAsync(request);  
        }

        public async Task<HttpResponseMessage> GetBooks(int id)
        {
            var request = GetRequestMessage(null, $"api/v1/Books/{id}", HttpMethod.Get, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostBooks(Book book)
        {
            var request = GetRequestMessage(book, "api/v1/Books/", HttpMethod.Post, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PutBooks(Book book, int id)
        {
            var request = GetRequestMessage(book, $"api/v1/Books/{id}", HttpMethod.Put, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> DeleteBooks(int id)
        {
            var request = GetRequestMessage(null, $"api/v1/Books/{id}", HttpMethod.Delete, _configuration);
            return await client.SendAsync(request);
        }
    }
}
