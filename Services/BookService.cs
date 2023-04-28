using Microsoft.EntityFrameworkCore;
using WebLibrary.API.Data;

namespace WebLibrary.API.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(b => b.Author).Include(b => b.Categories).ToListAsync();
        }
    }
}
