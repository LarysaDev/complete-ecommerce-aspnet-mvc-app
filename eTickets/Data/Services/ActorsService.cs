using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task AddAsync (Actor actor)
        {
             await _context.Actors.AddAsync(actor); 
             await  _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var actors = await _context.Actors.ToListAsync();
            return actors;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var results = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
            return results;
        }

        public async Task<Actor> UpdateAsync(int id, Actor newActor)
        {
            _context.Update(newActor);
            await _context.SaveChangesAsync();
            return newActor;
        }
    }
}
