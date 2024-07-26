using TPBoardWebApi.Models;
using TPBoardWebApi.Repositories;

namespace TPBoardWebApi.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByLogin(string login);
        public User GetUserByName(string name);
        bool VerifyUser(string login, string password);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool UserExists(string login);
        public bool EmailExists(string emial);

    }
}
