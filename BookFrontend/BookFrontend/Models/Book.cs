using System.ComponentModel.DataAnnotations;

namespace BookFrontend.Models
{
    public class Book
    {
        [Required]
        public int id { get; set; }
        
        [Required]
        public string ?title { get; set; }        
        public string ?description { get; set; }
        public int pageCount { get; set; }
        [Required]
        public string ?excerpt { get; set; }
        public DateTime publishDate { get; set; }

    }
}
