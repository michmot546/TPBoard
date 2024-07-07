using TPBoardWebApi.Models;

namespace TPBoardWebApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Projects { get; }
        IRepository<Table> Tables { get; }
        IRepository<TableElement> TableElements{ get; }
        IRepository<User> Users{ get; }
        IRepository<ProjectUser> ProjectUsers { get; }

        void Save();
    }
}
