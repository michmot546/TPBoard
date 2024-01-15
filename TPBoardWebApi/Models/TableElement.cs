namespace TPBoardWebApi.Models
{
    public class TableElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Table Table { get; set; }
    }
}
