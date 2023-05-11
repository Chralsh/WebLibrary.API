using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
