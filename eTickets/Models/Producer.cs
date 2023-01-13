using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture URL")]
        [Required(ErrorMessage = "Profile picture is required")]
        public string ProfilePictureULR { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name has to be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Bio")]
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }
        
        //Relationships
        //1 producer can have multiple movies
        public List<Movie> Movies { get; set; }
    }
}
 