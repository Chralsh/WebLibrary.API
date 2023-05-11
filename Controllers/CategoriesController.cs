using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Controllers.Policies;
using WebLibrary.API.Models;
using WebLibrary.API.Services;

namespace WebLibrary.API.Controllers
{
    [Authorize]
    [ApiVersion("7.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategoriesAsync());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
            var createdCategory = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await _categoryService.UpdateCategoryAsync(id,category);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
