using System.Linq.Expressions;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly TPBoardDbContext _context;

        public UserRepository(TPBoardDbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            SaveChanges(entity);
        }

        public User FirstOrDefault(Expression<Func<User, bool>> predicate)
        {
            var User = _context.Users.FirstOrDefault(predicate);

            if (User == null)
            {
                throw new InvalidOperationException("No User matches the specified condition.");
            }

            return User;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            var User = _context.Users.Find(id);

            if (User == null)
            {
                throw new InvalidOperationException($"User with ID {id} does not exist.");
            }

            return User;
        }

        public void SaveChanges(User entity)
        {
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            SaveChanges(entity);
        }
    }
}
