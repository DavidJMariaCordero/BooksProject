using BookFrontend.Models;
using BookFrontend.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System.Text.Json;

namespace BookFrontend.Pages
{
    public partial class Books
    {
        List<Book> books = new();
        public Book book { get; set; } = new();
        public int BookId { get; set; }
        public bool ShowBookForm { get; set; }
        public bool ShowDetail { get; set; }
        public bool isEdit { get; set; }
        public int OldId { get; set; }
        [Inject]
        public IBookApi bookApi { get; set; }
        [Inject] 
        public NotificationService Notification { get; set; }
        public HttpResponseMessage response { get; set; } = new();
       


        private async Task Search()
        {
            if(BookId == 0)
            {
                response = await bookApi.GetBooks();
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadAsStringAsync();
                    books = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                }
                else
                    Notification.Notify(NotificationSeverity.Error, "Error", $"HttpResponse[{response.StatusCode}]");
            }                    
            else
            {
                response = await bookApi.GetBooks(BookId);

                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadAsStringAsync();
                    book = System.Text.Json.JsonSerializer.Deserialize<Book>(respuesta, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    books.Add(book);
                    StateHasChanged();
                }
                else
                    Notification.Notify(NotificationSeverity.Error, "Error", $"HttpResponse[{response.StatusCode} ({response.Content})]");
            }

              
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public async Task Delete(int id)
        {
            if (id != 0)
            {
                response = await bookApi.DeleteBooks(id);
                if (response.IsSuccessStatusCode)
                {
                    Notification.Notify(NotificationSeverity.Error, "Error", $"Success");
                }
                else
                    Notification.Notify(NotificationSeverity.Error, "Error", $"HttpResponse[{response.StatusCode}]");
            }
        }
        public void Edit(int id)
        {
            isEdit = true;
            book = books.Where(x => x.id == id).FirstOrDefault();
            OldId = id;
            ShowBookForm = true;
        }

        public async Task DetailsAsync(int id)
        {
            BookId = id;
            await Search();
            ShowDetail = true;
            StateHasChanged();
        }

        public void CloseDetail()
        {
            BookId = 0;
            ShowDetail = false;
            StateHasChanged();
        }

        public void AddBook()
        {
            ShowBookForm = true;
        }

        public async Task Submit(Book book)
        {
            try
            {
                response = !isEdit ?  await bookApi.PostBooks(book) : await bookApi.PutBooks(book, OldId);
                if (response.IsSuccessStatusCode)
                {
                    Notification.Notify(NotificationSeverity.Success, "Good", $"Success");
                }
                else
                    Notification.Notify(NotificationSeverity.Error, "Error", $"HttpResponse[{response.StatusCode}]");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async void Cancel()
        {
            book = new();
            books.Clear();
            OldId = 0;
            ShowBookForm = false;
            await Search();
            StateHasChanged();
        }
    }
}
