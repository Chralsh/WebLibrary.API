using System.ComponentModel.DataAnnotations;
using WebLibrary.API.Models;

namespace WebLibrary.API.DTOs
{
    public class BookDTO
    {
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public int AuthorId { get; set; }
        [Required]
        [StringLength(255)]
        public AuthorDTO Author { get; set; }
        [Required]
        [StringLength(255)]
        public ICollection<CategoryDTO> Categories { get; set; }
    }
}
