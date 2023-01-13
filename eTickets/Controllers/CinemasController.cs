using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allPCinemas = await _service.GetAllAsync();
            return View(allPCinemas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Cinema cinema)
        {
            //  if (!ModelState.IsValid)
            //   {
            //     return View(actor);
            //  }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            else return View(details);

        }
        //Get: Actors/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            return View(details);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmation(int id, [Bind("Id,Logo,Name,Description")]Cinema cinema)
        {
            //  if (!ModelState.IsValid)
            //  {
            //      return View("NotFound");
            // }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            return View(details);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
