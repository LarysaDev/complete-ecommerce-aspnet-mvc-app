using eTickets.Models;

namespace eTickets.Data.ViewModels
{
    public class NewMoviesDropdownsVM
    {
        public List<Producer> Producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
        public NewMoviesDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }
    }
}
