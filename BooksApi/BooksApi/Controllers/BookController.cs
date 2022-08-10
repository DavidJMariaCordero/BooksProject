using BooksApi.BookServices;
using BooksApi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BooksApi.Controllers
{
    
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Books")]
    public class BookController : ControllerBase
    {
        [Inject]
        public IBookService bookService { get; set; }

        public BookController(IBookService _bookService)
        {
            bookService = _bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            var response = await bookService.GetBooks();
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                var bookList = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return Ok(bookList);
            }
            else
                return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            var response = await bookService.GetBooks(id);
            if (response.IsSuccessStatusCode)
            {
                var respuesta = await response.Content.ReadAsStringAsync();
                var book = System.Text.Json.JsonSerializer.Deserialize<Book>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return Ok(book);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> PostBooks(Book book)
        {
            var response = await bookService.PostBooks(book);
            if (response.IsSuccessStatusCode)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutBooks(Book book, int id)
        {
            var response = await bookService.PutBooks(book, id);
            if (response.IsSuccessStatusCode)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooks(int id)
        {
            var response = await bookService.DeleteBooks(id);
            if (response.IsSuccessStatusCode)
                return Ok();
            else
                return BadRequest();
        }

    }
}
