using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;

    public ProjectController(IProjectService projectService, IUserService userService)
    {
        _projectService = projectService;
        _userService = userService;
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetAllProjects")]
    public IActionResult GetAllProjects()
    {
        if (User.IsInRole("Admin"))
        {
            var projects = _projectService.GetAllProjects();
            return Ok(projects);
        }
        else
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var projects = _projectService.GetProjectsByUserId(userId);
            return Ok(projects);
        }
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetProjectById/{id}")]
    public IActionResult GetProjectById(int id)
    {
        var project = _projectService.GetProjectById(id);

        if (project == null)
        {
            return NotFound();
        }

        if (User.IsInRole("Admin") || _projectService.IsUserInProject(id, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)))
        {
            return Ok(project);
        }

        return Forbid();
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetUserProjects")]
    public IActionResult GetUserProjects()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var projects = _projectService.GetProjectsByUserId(userId);
        return Ok(projects);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("CreateProject")]
    public IActionResult CreateProject([FromBody] Project newProject)
    {
        if (newProject == null)
        {
            return BadRequest("Invalid project data");
        }

        _projectService.CreateProject(newProject);

        return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, newProject);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("CreateProjectWithOwnerAdded")]
    public IActionResult CreateProjectWithOwnerAdded([FromBody] Project newProject)
    {
        if (newProject == null)
        {
            return BadRequest("Invalid project data");
        }

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        _projectService.CreateProjectWithOwnerAdded(newProject, userId);

        return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, newProject);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("AddUserToProject")]
    public IActionResult AddUserToProject([FromBody] AddRemoveUserDto dto)
    {
        var project = _projectService.GetProjectById(dto.ProjectId);
        if (project == null)
        {
            return NotFound();
        }

        var user = _userService.GetUserById(dto.UserId);
        if (user == null)
        {
            return NotFound();
        }

        _projectService.AddUserToProject(dto.UserId, dto.ProjectId);
        return Ok();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPost("RemoveUserFromProject")]
    public IActionResult RemoveUserFromProject([FromBody] AddRemoveUserDto dto)
    {
        var project = _projectService.GetProjectById(dto.ProjectId);
        if (project == null)
        {
            return NotFound();
        }

        var user = _userService.GetUserById(dto.UserId);
        if (user == null)
        {
            return NotFound();
        }

        _projectService.RemoveUserFromProject(dto.UserId, dto.ProjectId);
        return Ok();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPut("UpdateProject/{id}")]
    public IActionResult UpdateProject(int id, [FromBody] Project updatedProject)
    {
        if (id != updatedProject.Id)
        {
            return BadRequest("Invalid project data");
        }

        _projectService.UpdateProject(updatedProject);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpDelete("DeleteProject/{id}")]
    public IActionResult DeleteProject(int id)
    {
        var project = _projectService.GetProjectById(id);

        if (project == null)
        {
            return NotFound();
        }

        _projectService.DeleteProject(id);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetProjectMembers/{projectId}")]
    public IActionResult GetProjectMembers(int projectId)
    {
        if (User.IsInRole("Admin") || _projectService.IsUserInProject(projectId, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)))
        {
            try
            {
                var members = _projectService.GetProjectMembers(projectId);
                return Ok(members);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        return Forbid();
    }
}
