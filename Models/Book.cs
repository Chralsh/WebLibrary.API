namespace WebLibrary.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
