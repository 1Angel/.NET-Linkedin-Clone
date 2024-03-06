namespace LinkedinClone.Models
{
    public class JobsAppByUser
    {
        public int Id { get; set; }
        public User User { get; set; }
        public JobPost JobPost { get; set; }
    }
}
