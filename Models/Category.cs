namespace WebLibrary.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
