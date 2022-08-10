using BooksApi.BookServices;
using BooksApi.Controllers;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class BookTest 
    {
        private readonly BookController _controller;
        private readonly IBookService _service;
        public BookTest()
        {
            _service = new BookService(new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build());
            _controller = new BookController(_service);
        }
        [Fact]
        public async Task Get_BooksAsync()
        {
            var result = await _controller.GetBooks();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_BooksByIdAsync()
        {
            var result = await _controller.GetBooks(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PostBooksAsync()
        {
            Book book = new() { id = 1, description = "Hello people this is my beautiful book!", excerpt = "jdkdjkdjkdjdkj", pageCount = 1, publishDate = DateTime.Now, title = "Life as a software developer at NewTech" };
            var result = await _controller.PostBooks(book);

            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public async Task PutBooksAsync()
        {
            Book book = new() { id = 1, description = "Hello people this is my beautiful book!", excerpt = "jdkdjkdjkdjdkj", pageCount = 1, publishDate = DateTime.Now, title = "Life as a software developer at NewTech" };
            var result = await _controller.PutBooks(book, 1);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteBooksAsync()
        {
            var result = await _controller.DeleteBooks(1);

            Assert.IsType<OkResult>(result);
        }
    }
}