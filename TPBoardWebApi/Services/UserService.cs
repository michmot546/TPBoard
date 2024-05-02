using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Services
{
    public class UserService :IUserService
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

        public void CreateUser(User user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.Save();
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
    }
}
