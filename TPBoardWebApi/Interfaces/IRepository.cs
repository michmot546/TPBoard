using System.Linq.Expressions;

namespace TPBoardWebApi.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges(TEntity entity);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
