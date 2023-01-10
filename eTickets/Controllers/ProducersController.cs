using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly AppDbContext _context;
        public ProducersController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        //Inside an async function, you can use the await keyword before a call to a function that returns a promise.
        //This makes the code wait at that point until
        //the promise is settled, at which point the fulfilled value of the promise is treated as a return value,
        //or the rejected value is thrown.
        public async Task<IActionResult> Index()
        {
            var allProducers = await _context.Producers.ToListAsync();
            return View();
        }
    }
}
