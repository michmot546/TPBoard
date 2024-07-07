using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TPBoardWebApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class Project
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        [JsonIgnore]
        public ICollection<ProjectUser> Users { get; set; }
        public ICollection<Table>? Tables { get; set; } = null;
    }
}
