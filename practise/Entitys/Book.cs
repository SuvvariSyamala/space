using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace practise.Entitys
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string?  Title { get; set; }

        public string? Author { get; set; }
        [Required]
        public string? Genre { get; set; }

       // public string Image { get; set; }

        public string  ISBN { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User  Usernew { get; set; }

    }
}
