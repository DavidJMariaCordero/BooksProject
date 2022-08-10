using BooksApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Services.BookServices
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
    }
}
