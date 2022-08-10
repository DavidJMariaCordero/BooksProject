using BookFrontend.Models;

namespace BookFrontend.Services
{
    public interface IBookApi
    {
        Task<HttpResponseMessage> GetBooks();
        Task<HttpResponseMessage> GetBooks(int id);
        Task<HttpResponseMessage> PostBooks(Book book);
        Task<HttpResponseMessage> PutBooks(Book book, int id);
        Task<HttpResponseMessage> DeleteBooks(int id);
    }
}
