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
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _bookService.GetAllBooksAsync());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var createdBook = await _bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            await _bookService.UpdateBookAsync(id, book);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("author/{authorId}")]
        public async Task<ActionResult<Book>> GetBooksByAuthor(int authorId)
        {
            var books = await _bookService.GetBooksByAuthorAsync(authorId);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<ActionResult<Book>> GetBooksByCategory(int categoryId)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryId);

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }
        [Authorize]
        [HttpPost("{authorId}")]
        public async Task<ActionResult<Book>> AddBookForAuthor(int authorId, Book book)
        {
            await _bookService.AddBookForAuthorAsync(authorId, book);
            return StatusCode(StatusCodes.Status201Created, book);
        }
        private async Task<bool> BookExists(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book != null;
        }

    }
}