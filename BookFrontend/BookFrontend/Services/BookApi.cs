using BookFrontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BookFrontend.Services
{
    public class BookApi : IBookApi
    {
        protected readonly IConfiguration _configuration;
        HttpClient client = new();
        public BookApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static HttpRequestMessage GetRequestMessage(Book data, string ruta, HttpMethod accion, IConfiguration configuration)
        {
            var json = JsonConvert.SerializeObject(data);
            var url = configuration["ApiUrl"] + ruta;
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
            var request = GetRequestMessage(null, "api/Books", HttpMethod.Get, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetBooks(int id)
        {
            var request = GetRequestMessage(null, $"api/Books/{id}", HttpMethod.Get, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PostBooks(Book book)
        {
            var request = GetRequestMessage(book, "api/Books/", HttpMethod.Post, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PutBooks(Book book, int id)
        {
            var request = GetRequestMessage(book, $"api/Books/{id}", HttpMethod.Put, _configuration);
            return await client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> DeleteBooks(int id)
        {
            var request = GetRequestMessage(null, $"api/Books/{id}", HttpMethod.Delete, _configuration);
            return await client.SendAsync(request);
        }
    }
}
