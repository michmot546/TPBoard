using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace TPBoardWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.GetAll();
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.Users.GetById(id);
        }

        public User GetUserByLogin(string login)
        {
            return _unitOfWork.Users.FirstOrDefault(u => u.Login == login);
        }

        public void CreateUser(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
        }

        public bool VerifyUser(string login, string password)
        {
            var user = GetUserByLogin(login);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public void UpdateUser(User user)
        {
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }

        public void DeleteUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                _unitOfWork.Users.Delete(user);
                _unitOfWork.Save();
            }
        }
        public bool UserExists(string login)
        {
            return _unitOfWork.Users.Any(u => u.Login == login);
        }

        public bool EmailExists(string emial)
        {
            return _unitOfWork.Users.Any(u => u.Email == emial);
        }
    }
}
