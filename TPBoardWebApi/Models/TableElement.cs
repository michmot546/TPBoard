using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TPBoardWebApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class TableElement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TableId { get; set; }
        public int? AssignedUserId { get; set; }
        //public User AssignedUser { get; set; }
    }
}
