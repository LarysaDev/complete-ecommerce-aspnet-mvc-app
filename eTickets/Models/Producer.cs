using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePictureULR { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        
        //Relationships
        //1 producer can have multiple movies
        public List<Movie> Movies { get; set; }
    }
}
 