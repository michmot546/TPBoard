namespace TPBoardWebApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
