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

        public void CreateProjectWithOwnerAdded(Project project, int ownerId)
        {
            _unitOfWork.Projects.Add(project);
            _unitOfWork.Save();

            var projectUser = new ProjectUser
            {
                ProjectId = project.Id,
                UserId = ownerId
            };
            _unitOfWork.ProjectUsers.Add(projectUser);
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
                var relatedTables = _unitOfWork.Tables.Find(t => t.ProjectId == id).ToList();
                foreach (var table in relatedTables)
                {
                    _unitOfWork.Tables.Delete(table);
                }

                _unitOfWork.Projects.Delete(project);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            return _unitOfWork.Projects
                              .Find(p => p.OwnerId == userId || p.Users.Any(u => u.UserId == userId))
                              .ToList();
        }

        public IEnumerable<User> GetProjectMembers(int projectId)
        {
            var project = _unitOfWork.Projects.GetById(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException("Project not found");
            }

            var projectUsers = _unitOfWork.ProjectUsers.Find(pu => pu.ProjectId == projectId).ToList();

            if (!projectUsers.Any())
            {
                throw new KeyNotFoundException("No users found for this project");
            }
            var userIds = projectUsers.Select(pu => pu.UserId).ToList();
            var users = _unitOfWork.Users.Find(u => userIds.Contains(u.Id)).ToList();

            return users;
        }

        public void AddUserToProject(int userId, int projectId)
        {
            var projectUser = new ProjectUser
            {
                UserId = userId,
                ProjectId = projectId
            };
            _unitOfWork.ProjectUsers.Add(projectUser);
            _unitOfWork.Save();
        }

        public void RemoveUserFromProject(int userId, int projectId)
        {
            var projectUser = _unitOfWork.ProjectUsers.FirstOrDefault(pu => pu.UserId == userId && pu.ProjectId == projectId);
            if (projectUser != null)
            {
                _unitOfWork.ProjectUsers.Delete(projectUser);
                _unitOfWork.Save();
            }
        }
    }
}
