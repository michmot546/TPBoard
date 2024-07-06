using System.Linq;
using System.Linq.Expressions;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Repositories
{
    public class TableElementRepository : IRepository<TableElement>
    {
        private readonly TPBoardDbContext _context;

        public TableElementRepository(TPBoardDbContext context)
        {
            _context = context;
        }

        public void Add(TableElement entity)
        {
            _context.TableElements.Add(entity);
        }

        public bool Any(Expression<Func<TableElement, bool>> predicate)
        {
            return _context.TableElements.Any(predicate);
        }

        public void Delete(TableElement entity)
        {
            _context.TableElements.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<TableElement> Find(Expression<Func<TableElement, bool>> predicate)
        {
            return _context.TableElements.Where(predicate).ToList();
        }

        public TableElement FirstOrDefault(Expression<Func<TableElement, bool>> predicate)
        {
            var TableElement = _context.TableElements.FirstOrDefault(predicate);

            if (TableElement == null)
            {
                throw new InvalidOperationException("No TableElement matches the specified condition.");
            }

            return TableElement;
        }

        public IEnumerable<TableElement> GetAll()
        {
            return _context.TableElements.ToList();
        }

        public TableElement GetById(int id)
        {
            var TableElement = _context.TableElements.Find(id);

            if (TableElement == null)
            {
                throw new InvalidOperationException($"TableElement with ID {id} does not exist.");
            }

            return TableElement;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(TableElement entity)
        {
            _context.TableElements.Update(entity);
            SaveChanges();
        }
    }
}
