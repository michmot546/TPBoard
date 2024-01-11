namespace TPBoardWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
