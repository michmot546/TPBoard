namespace TPBoardWebApi.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        public ICollection<TableElement> Elements { get; set; }
    }
}
