using System.Linq.Expressions;
using System.Collections.Generic;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class ProjectUserRepository : IRepository<ProjectUser>
    {
        private readonly TPBoardDbContext _context;

        public ProjectUserRepository(TPBoardDbContext context)
        {
            _context = context;
        }

        public void Add(ProjectUser entity)
        {
            _context.ProjectUser.Add(entity);
            SaveChanges();
        }

        public bool Any(Expression<Func<ProjectUser, bool>> predicate)
        {
            return _context.ProjectUser.Any(predicate);
        }

        public void Delete(ProjectUser entity)
        {
            _context.ProjectUser.Remove(entity);
            SaveChanges();
        }

        public ProjectUser FirstOrDefault(Expression<Func<ProjectUser, bool>> predicate)
        {
            var projectUser = _context.ProjectUser.FirstOrDefault(predicate);

            if (projectUser == null)
            {
                throw new InvalidOperationException("No project user matches the specified condition.");
            }

            return projectUser;
        }

        public IEnumerable<ProjectUser> GetAll()
        {
            return _context.ProjectUser.ToList();
        }

        public ProjectUser GetById(int id)
        {
            var projectUser = _context.ProjectUser.Find(id);

            if (projectUser == null)
            {
                throw new InvalidOperationException($"ProjectUser with ID {id} does not exist.");
            }

            return projectUser;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(ProjectUser entity)
        {
            _context.ProjectUser.Update(entity);
            SaveChanges();
        }

        public IEnumerable<ProjectUser> Find(Expression<Func<ProjectUser, bool>> predicate)
        {
            return _context.ProjectUser.Where(predicate).ToList();
        }
    }
}
