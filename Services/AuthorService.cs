using Microsoft.EntityFrameworkCore;
using WebLibrary.API.Data;
using WebLibrary.API.Models;

namespace WebLibrary.API.Services
{
    public class AuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }
        public async Task<Author> UpdateAuthorAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                throw new ApplicationException("Author not found.");
            }
            _context.Entry(author).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    throw new ApplicationException("Author not found.");
                }
                else
                {
                    throw;
                }
            }
            return author;
        }

        public async Task<Author> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new ApplicationException("Author not found.");
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

    }
}