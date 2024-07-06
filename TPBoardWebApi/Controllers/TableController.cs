using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;
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
    [Authorize]
    [HttpGet("GetAllTables")]
    public IActionResult GetAllTables()
    {
        var tables = _tableService.GetAllTables();
        return Ok(tables);
    }
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
