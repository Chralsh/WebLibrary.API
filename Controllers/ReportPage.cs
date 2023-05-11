using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLibrary.API.Data;

namespace WebLibrary.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportPageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAuthorsByBookCount()
        {
            var authorsByBookCount = _context.Authors
                .Include(a => a.Books)
                .OrderByDescending(a => a.Books.Count)
                .Select(author => $"{author.Name}: {author.Books.Count} books")
                .ToList();

            return Ok(authorsByBookCount);
        }
    }
}
