using System.ComponentModel.DataAnnotations;

public class UserLoginDto
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}
