using System.ComponentModel.DataAnnotations;
using WebLibrary.API.Models;

namespace WebLibrary.API.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public ICollection<BookDTO> Books { get; set; }
    }
}
