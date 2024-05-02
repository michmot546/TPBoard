using TPBoardWebApi.Models;

namespace TPBoardWebApi.Interfaces
{
    public interface ITableElementService
    {
        IEnumerable<TableElement> GetAllTableElements();
        TableElement GetTableElementById(int id);
        void CreateTableElement(TableElement element);
        void UpdateTableElement(TableElement element);
        void DeleteTableElement(int id);
    }
}
