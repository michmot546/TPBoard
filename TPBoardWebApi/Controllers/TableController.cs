using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TableController : Controller
{
    private readonly ITableService _tableService;

    public TableController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetAllTables")]
    public IActionResult GetAllTables()
    {
        var tables = _tableService.GetAllTables();
        return Ok(tables);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetTableById/{id}")]
    public IActionResult GetTableById(int id)
    {
        var table = _tableService.GetTableById(id);

        if (table == null)
        {
            return NotFound();
        }

        return Ok(table);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetTablesByProjectId/{projectId}")]
    public IActionResult GetTablesByProjectId(int projectId)
    {
        var tables = _tableService.GetTablesByProjectId(projectId);
        return Ok(tables);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpPost("CreateTable")]
    public IActionResult CreateTable([FromBody] Table newTable)
    {
        if (newTable == null)
        {
            return BadRequest("Invalid table data");
        }

        _tableService.CreateTable(newTable);

        return CreatedAtAction(nameof(GetTableById), new { id = newTable.Id }, newTable);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpPut("UpdateTable/{id}")]
    public IActionResult UpdateTable(int id, [FromBody] Table updatedTable)
    {
        if (id != updatedTable.Id)
        {
            return BadRequest("Invalid table data");
        }

        _tableService.UpdateTable(updatedTable);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpDelete("DeleteTable/{id}")]
    public IActionResult DeleteTable(int id)
    {
        var table = _tableService.GetTableById(id);

        if (table == null)
        {
            return NotFound();
        }

        _tableService.DeleteTable(id);

        return NoContent();
    }
}
