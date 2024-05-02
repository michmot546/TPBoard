using TPBoardWebApi.Models;

namespace TPBoardWebApi.Interfaces
{
    public interface ITableService
    {
        IEnumerable<Table> GetAllTables();
        Table GetTableById(int id);
        void CreateTable(Table table);
        void UpdateTable(Table table);
        void DeleteTable(int id);
    }
}
