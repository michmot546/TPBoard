using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Table> GetAllTables()
        {
            return _unitOfWork.Tables.GetAll();
        }

        public Table GetTableById(int id)
        {
            return _unitOfWork.Tables.GetById(id);
        }

        public void CreateTable(Table table)
        {
            _unitOfWork.Tables.Add(table);
            _unitOfWork.Save();
        }

        public void UpdateTable(Table table)
        {
            _unitOfWork.Tables.Update(table);
            _unitOfWork.Save();
        }

        public void DeleteTable(int id)
        {
            var table = _unitOfWork.Tables.GetById(id);
            if (table != null)
            {
                _unitOfWork.Tables.Delete(table);
                _unitOfWork.Save();
            }
        }
    }
}
