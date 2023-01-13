using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        //Inside an async function, you can use the await keyword before a call to a function that returns a promise.
        //This makes the code wait at that point until
        //the promise is settled, at which point the fulfilled value of the promise is treated as a return value,
        //or the rejected value is thrown.
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }
        public async Task<IActionResult> Details(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            else return View(details);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureULR,Bio")]Producer producer)
        {
            //  if (!ModelState.IsValid)
            //   {
            //     return View(actor);
            //  }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetByIdAsync(id);
            if (details == null) return View("NotFound");
            return View(details);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmation(int id, [Bind("Id,FullName,ProfilePictureULR,Bio")] Producer producer)
        {
            //  if (!ModelState.IsValid)
            //  {
            //      return View("NotFound");
            // }
            await _service.UpdateAsync(id, producer);
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
