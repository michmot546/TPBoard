using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        private readonly TPBoardDbContext _context;

        public ProjectRepository(TPBoardDbContext context)
        {
            _context = context;
        }

        public void Add(Project entity)
        {
            _context.Projects.Add(entity);
        }

        public bool Any(Expression<Func<Project, bool>> predicate)
        {
            return _context.Projects.Any(predicate);
        }

        public void Delete(Project entity)
        {
            _context.Projects.Remove(entity);
            SaveChanges();
        }

        public Project FirstOrDefault(Expression<Func<Project, bool>> predicate)
        {
            var project = _context.Projects.FirstOrDefault(predicate);

            if (project == null)
            {
                throw new InvalidOperationException("No project matches the specified condition.");
            }

            return project;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public Project GetById(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                throw new InvalidOperationException($"Project with ID {id} does not exist.");
            }

            return project;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Project entity)
        {
            _context.Projects.Update(entity);
            SaveChanges();
        }

        public IEnumerable<Project> Find(Expression<Func<Project, bool>> predicate)
        {
            return _context.Projects.Include(p => p.Users).Where(predicate).ToList();
        }

    }
}
