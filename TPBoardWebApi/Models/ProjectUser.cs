namespace TPBoardWebApi.Models
{
    public class ProjectUser
    {
        public int UsertId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
