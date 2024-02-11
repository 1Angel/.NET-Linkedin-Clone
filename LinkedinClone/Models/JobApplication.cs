using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkedinClone.Models
{
    [Table("jobs-aplications")]
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string CurriculumUrl { get; set; }
        public int JobPostId { get; set; }
        public JobPost JobPost { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
