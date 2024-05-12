using System.Linq.Expressions;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class TableRepository : IRepository<Table>
    {
        private readonly TPBoardDbContext _context;

        public TableRepository(TPBoardDbContext context)
        {
            _context = context;
        }

        public void Add(Table entity)
        {
            _context.Tables.Add(entity);
        }

        public void Delete(Table entity)
        {
            _context.Tables.Remove(entity);
            SaveChanges();
        }

        public Table FirstOrDefault(Expression<Func<Table, bool>> predicate)
        {
            var Table = _context.Tables.FirstOrDefault(predicate);

            if (Table == null)
            {
                throw new InvalidOperationException("No Table matches the specified condition.");
            }

            return Table;
        }

        public IEnumerable<Table> GetAll()
        {
            return _context.Tables.ToList();
        }

        public Table GetById(int id)
        {
            var Table = _context.Tables.Find(id);

            if (Table == null)
            {
                throw new InvalidOperationException($"Table with ID {id} does not exist.");
            }

            return Table;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Table entity)
        {
            _context.Tables.Update(entity);
            SaveChanges();
        }
    }
}
