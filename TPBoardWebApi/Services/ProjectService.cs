using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _unitOfWork.Projects.GetAll();
        }

        public Project GetProjectById(int id)
        {
            return _unitOfWork.Projects.GetById(id);
        }

        public void CreateProject(Project project)
        {
            _unitOfWork.Projects.Add(project);
            _unitOfWork.Save();
        }

        public void UpdateProject(Project project)
        {
            _unitOfWork.Projects.Update(project);
            _unitOfWork.Save();
        }

        public void DeleteProject(int id)
        {
            var project = _unitOfWork.Projects.GetById(id);
            if (project != null)
            {
                _unitOfWork.Projects.Delete(project);
                _unitOfWork.Save();
            }
        }
    }
}
