using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Services
{
    public class TableElementService : ITableElementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableElementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TableElement> GetAllTableElements()
        {
            return _unitOfWork.TableElements.GetAll();
        }

        public TableElement GetTableElementById(int id)
        {
            return _unitOfWork.TableElements.GetById(id);
        }

        public void CreateTableElement(TableElement element)
        {
            _unitOfWork.TableElements.Add(element);
            _unitOfWork.Save();
        }

        public void UpdateTableElement(TableElement element)
        {
            _unitOfWork.TableElements.Update(element);
            _unitOfWork.Save();
        }

        public void DeleteTableElement(int id)
        {
            var element = _unitOfWork.TableElements.GetById(id);
            if (element != null)
            {
                _unitOfWork.TableElements.Delete(element);
                _unitOfWork.Save();
            }
        }
    }
}
