using TPBoardWebApi.Models;

namespace TPBoardWebApi.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProjectById(int id);
        void CreateProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
        public IEnumerable<Project> GetProjectsByUserId(int userId);
    }
}
