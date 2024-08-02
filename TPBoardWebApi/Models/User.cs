using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TPBoardWebApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Login needed.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password needed.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        [JsonIgnore]
        public ICollection<ProjectUser>? Projects { get; set; }
    }
}
