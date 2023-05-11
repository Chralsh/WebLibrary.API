using Microsoft.EntityFrameworkCore;
using WebLibrary.API.Models;
using WebLibrary.API.Data;

namespace WebLibrary.API.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.Include(book => book.Author).Include(b => b.Categories).ToListAsync();
        }
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(book => book.Author).Include(b => b.Categories).FirstOrDefaultAsync(book => book.Id == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBookAsync(int id, Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(book => book.Id == id);
            if (existingBook == null)
            {
                throw new InvalidOperationException($"Book with id {id} not found");
            }

            existingBook.Name = book.Name;
            existingBook.Description = book.Description;
            existingBook.AuthorId = book.AuthorId;
            existingBook.Image = book.Image;
            existingBook.Categories = book.Categories;
            _context.SaveChanges();
            return existingBook;
        }

        public async Task<Book> DeleteBookAsync(int id)
        {
            var existingBook = _context.Books.FirstOrDefault(book => book.Id == id);
            if (existingBook == null)
            {
                throw new InvalidOperationException($"Book with id {id} not found");
            }

            _context.Books.Remove(existingBook);
            _context.SaveChanges();

            return existingBook;
        }
        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books.Where(book => book.AuthorId == authorId).ToListAsync();
        }
        public async Task<List<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books.Where(book => book.Categories.Any(category => category.Id == categoryId)).ToListAsync();
        }
        public async Task<Book> AddBookForAuthorAsync(int authorId, Book book)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
            if (author == null)
            {
                throw new Exception($"Author with id {authorId} not found.");
            }
            book.AuthorId = authorId;
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
