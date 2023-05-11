using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Models;
using WebLibrary.API.Services;

namespace WebLibrary.API.Controllers
{
    [Authorize]
    [ApiVersion("7.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return Ok(await _authorService.GetAuthorsAsync());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Author>> AddAuthor(Author author)
        {
            var createdAuthor = await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.Id }, createdAuthor);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            await _authorService.UpdateAuthorAsync(id, author);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}