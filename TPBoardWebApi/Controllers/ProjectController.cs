using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : Controller
{
    private readonly TPBoardDbContext _TPBoardDbContext;

    public ProjectController(TPBoardDbContext TPBoardDbContext)
    {
        _TPBoardDbContext = TPBoardDbContext;
    }

    [HttpGet("GetAllProjects")]
    public IActionResult GetAllProjects()
    {
        var projects = _TPBoardDbContext.Projects.ToList();
        return Ok(projects);
    }

    [HttpGet("GetProjectById/{id}")]
    public IActionResult GetProjectById(int id)
    {
        var project = _TPBoardDbContext.Projects.Find(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(project);
    }

    [HttpPost("CreateProject")]
    public async Task<IActionResult> CreateProject([FromBody] Project newProject)
    {
        if (newProject == null)
        {
            return BadRequest("Invalid project data");
        }

        _TPBoardDbContext.Projects.Add(newProject);
        await _TPBoardDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, newProject);
    }

    [HttpPut("UpdateProject/{id}")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] Project updatedProject)
    {
        if (id != updatedProject.Id)
        {
            return BadRequest("Invalid project data");
        }

        _TPBoardDbContext.Entry(updatedProject).State = EntityState.Modified;

        try
        {
            await _TPBoardDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_TPBoardDbContext.Projects.Any(p => p.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("DeleteProject/{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _TPBoardDbContext.Projects.FindAsync(id);

        if (project == null)
        {
            return NotFound();
        }

        _TPBoardDbContext.Projects.Remove(project);
        await _TPBoardDbContext.SaveChangesAsync();

        return NoContent();
    }
}
