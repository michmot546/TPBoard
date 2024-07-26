using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TPBoardWebApi.Data;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;
using TPBoardWebApi.Services;

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

    [Authorize]
    [HttpGet("GetAllProjects")]
    public IActionResult GetAllProjects()
    {
        var projects = _projectService.GetAllProjects();
        return Ok(projects);
    }

    [Authorize]
    [HttpGet("GetProjectById/{id}")]
    public IActionResult GetProjectById(int id)
    {
        var project = _projectService.GetProjectById(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [Authorize]
    [HttpGet("GetUserProjects")]
    public IActionResult GetUserProjects()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var projects = _projectService.GetProjectsByUserId(userId);
        return Ok(projects);
    }


    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
    [HttpGet("GetProjectMembers/{projectId}")]
    public IActionResult GetProjectMembers(int projectId)
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

}