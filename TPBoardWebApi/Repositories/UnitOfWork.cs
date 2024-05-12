using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TPBoardDbContext _context;
        private bool _disposed;

        public UnitOfWork(TPBoardDbContext context)
        {
            _context = context;
            Projects = new ProjectRepository(_context);
            Tables = new TableRepository(_context);
            Users = new UserRepository(_context);
            TableElements = new TableElementRepository(_context);
        }

        public IRepository<Project> Projects { get; set; }
        public IRepository<Table> Tables { get; set; }
        public IRepository<TableElement> TableElements { get; set; }
        public IRepository<User> Users { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
