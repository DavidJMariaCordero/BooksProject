using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.BookServices
{
    public interface IBookService
    {
        Task<HttpResponseMessage> GetBooks();
        Task<HttpResponseMessage> GetBooks(int id);
        Task<HttpResponseMessage> PostBooks(Book book);
        Task<HttpResponseMessage> PutBooks(Book book, int id);
        Task<HttpResponseMessage> DeleteBooks(int id);
    }
}
