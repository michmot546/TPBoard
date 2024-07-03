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

        public bool Any(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Any(predicate);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            SaveChanges();
        }

        public User FirstOrDefault(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            SaveChanges();
        }
    }
}
