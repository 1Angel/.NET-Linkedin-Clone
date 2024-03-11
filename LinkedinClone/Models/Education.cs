using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 

    }
}
