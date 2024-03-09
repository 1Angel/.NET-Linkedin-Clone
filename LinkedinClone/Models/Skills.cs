using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }


    }
}
