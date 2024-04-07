using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TPBoardWebApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProjectId { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
        public ICollection<TableElement>? Elements { get; set; }
    }
}
