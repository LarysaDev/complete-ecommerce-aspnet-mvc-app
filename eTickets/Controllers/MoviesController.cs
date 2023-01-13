using eTickets.Data;
using eTickets.Data.Enums;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }
        public async Task<IActionResult> Details(int id)
        {
            var details = await _service.GetMovieByIdAsync(id);
            if (details == null) return View("NotFound");
            else return View(details);

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dropdownData = await _service.GetNewMoviesDropdownValues();
            ViewBag.CinemaId = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(dropdownData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(dropdownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovieVM)
        {
              if (!ModelState.IsValid)
               {
                var dropdownData = await _service.GetNewMoviesDropdownValues();
                ViewBag.CinemaId = new SelectList(dropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(dropdownData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(dropdownData.Actors, "Id", "FullName");
                return View(newMovieVM);
              }
            await _service.AddNewMovieAsync(newMovieVM);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null)
                return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                ProducerId = movieDetails.ProducerId,
                CinemaId = movieDetails.CinemaId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };
            var dropdownData = await _service.GetNewMoviesDropdownValues();
            ViewBag.CinemaId = new SelectList(dropdownData.Cinemas, "Id", "Name");
            ViewBag.ProducerId = new SelectList(dropdownData.Producers, "Id", "FullName");
            ViewBag.ActorId = new SelectList(dropdownData.Actors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");   
            if (!ModelState.IsValid)
            {
                var dropdownData = await _service.GetNewMoviesDropdownValues();
                ViewBag.CinemaId = new SelectList(dropdownData.Cinemas, "Id", "Name");
                ViewBag.ProducerId = new SelectList(dropdownData.Producers, "Id", "FullName");
                ViewBag.ActorId = new SelectList(dropdownData.Actors, "Id", "FullName");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            if (allMovies != null && !string.IsNullOrEmpty(searchString))
            {
                var filteredResults = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                if (filteredResults.Count > 0)
                {
                    ViewBag.Results = "Results were found";
                    return View("Index", filteredResults);
                }
                else
                {
                    ViewBag.Results = "Results were not found";
                    return View("Index", allMovies);
                }
            }
            return View("Index", allMovies);
        }

    }
}
