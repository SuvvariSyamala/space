using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace practise.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }

        
        public string? Title { get; set; }

        public string? Author { get; set; }
        
       // public string ? Name { get; set; }   
        public string? Genre { get; set; }

        //public string Image { get; set; }

        public int ISBN { get; set; }

        public DateTime PublishedDate { get; set; }

        public int UserId { get; set; }

    }
}
