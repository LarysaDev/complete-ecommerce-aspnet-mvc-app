using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
	public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext appDbContext) : base(appDbContext) {
            _context = appDbContext;
        }

        public async Task AddNewMovieAsync(NewMovieVM newMovieVM)
        {
            var newMovie = new Movie()
            {
                Name = newMovieVM.Name,
                Description = newMovieVM.Description,
                Price = newMovieVM.Price,
                StartDate = newMovieVM.StartDate,
                EndDate = newMovieVM.EndDate,
                ImageURL = newMovieVM.ImageURL,
                MovieCategory = newMovieVM.MovieCategory,         
                ProducerId = newMovieVM.ProducerId,
                CinemaId = newMovieVM.CinemaId
            };
           await _context.Movies.AddAsync(newMovie);
           await _context.SaveChangesAsync();

            //Add Movie actors
            foreach (var actorId in newMovieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMoviesDropdownsVM> GetNewMoviesDropdownValues()
        {
            var response = new NewMoviesDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
        };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM movie)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movie.Id);
            if (dbMovie != null)
            {
                dbMovie.Name = movie.Name;
                dbMovie.Description = movie.Description;
                dbMovie.Price = movie.Price;
                dbMovie.StartDate = movie.StartDate;
                dbMovie.EndDate = movie.EndDate;
                dbMovie.ImageURL = movie.ImageURL;
                dbMovie.MovieCategory = movie.MovieCategory;
                dbMovie.ProducerId = movie.ProducerId;
                dbMovie.CinemaId = movie.CinemaId;
                await _context.SaveChangesAsync();
            }

            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == movie.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie actors
            foreach (var actorId in movie.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
