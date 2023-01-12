using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            //Include() підгружає всі пов'язані з Cinama хедери цього класу, щоб можна було юзати item.Cinema.Name
            var allMovies = await _context.Movies.Include(n=>n.Cinema).OrderBy(n=>n.Name).ToListAsync();
            return View(allMovies);
        }
    }
}
