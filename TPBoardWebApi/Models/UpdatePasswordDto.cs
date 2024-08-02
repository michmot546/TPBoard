namespace TPBoardWebApi.Models
{
    public class UpdatePasswordDto
    {
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
