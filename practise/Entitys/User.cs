using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace practise.Entitys
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string?  UserName { get; set; }

        public string? Email { get; set; }
        [StringLength(50)]
        [Column("Password", TypeName = "varchar")]
        public string ? Password { get; set; }
        [StringLength(50)]
        [Column("Phone", TypeName = "varchar")]
        public string?   PhoneNumber { get; set; }

        public string ? Role { get; set; }
    }
}
