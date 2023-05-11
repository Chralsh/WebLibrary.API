using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
