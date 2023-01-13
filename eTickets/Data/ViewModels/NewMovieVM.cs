using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Display(Name ="Movie name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie describtion")]
        [Required(ErrorMessage = "Describtion is required")]
        public string Description { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Movie Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Movie Category is required")]
        public MovieCategory MovieCategory { get; set; }

        [Display(Name = "Movie poster url")]
        [Required(ErrorMessage = "Url is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Select actors")]
        [Required(ErrorMessage = "Actors are required")]
        public List<int> ActorIds { get; set; }

        [Display(Name = "Select cinema")]
        [Required(ErrorMessage = "Cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select producer")]
        [Required(ErrorMessage = "Producer is required")]
        public int ProducerId { get; set; }
    }
}
