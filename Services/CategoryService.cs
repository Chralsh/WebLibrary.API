using Microsoft.EntityFrameworkCore;
using WebLibrary.API.Data;
using WebLibrary.API.Models;

namespace WebLibrary.API.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new ApplicationException("Category not found.");
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    throw new ApplicationException("Category not found.");
                }
                else
                {
                    throw;
                }
            }

            return category;
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ApplicationException("Category not found.");
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }
        private bool CategoryExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}