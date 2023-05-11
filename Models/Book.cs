using System.ComponentModel.DataAnnotations;

namespace WebLibrary.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public int AuthorId { get; set; }
        [Required]
        [StringLength(255)]
        public Author Author { get; set; }
        [Required]
        [StringLength(255)]
        public ICollection<Category> Categories { get; set; }
    }
}
