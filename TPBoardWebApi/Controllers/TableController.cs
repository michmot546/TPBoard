using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TableController : Controller
{
    private readonly TPBoardDbContext _TPBoardDbContext;

    public TableController(TPBoardDbContext TPBoardDbContext)
    {
        _TPBoardDbContext = TPBoardDbContext;
    }

    [HttpGet("GetAllTables")]
    public IActionResult GetAllTables()
    {
        var tables = _TPBoardDbContext.Tables.ToList();
        return Ok(tables);
    }

    [HttpGet("GetTableById/{id}")]
    public IActionResult GetTableById(int id)
    {
        var table = _TPBoardDbContext.Tables.Find(id);

        if (table == null)
        {
            return NotFound();
        }

        return Ok(table);
    }

    [HttpPost("CreateTable")]
    public async Task<IActionResult> CreateTable([FromBody] Table newTable)
    {
        if (newTable == null)
        {
            return BadRequest("Invalid table data");
        }

        _TPBoardDbContext.Tables.Add(newTable);
        await _TPBoardDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTableById), new { id = newTable.Id }, newTable);
    }

    [HttpPut("UpdateTable/{id}")]
    public async Task<IActionResult> UpdateTable(int id, [FromBody] Table updatedTable)
    {
        if (id != updatedTable.Id)
        {
            return BadRequest("Invalid table data");
        }

        _TPBoardDbContext.Entry(updatedTable).State = EntityState.Modified;

        try
        {
            await _TPBoardDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_TPBoardDbContext.Tables.Any(t => t.Id == id))
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

    [HttpDelete("DeleteTable/{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var table = await _TPBoardDbContext.Tables.FindAsync(id);

        if (table == null)
        {
            return NotFound();
        }

        _TPBoardDbContext.Tables.Remove(table);
        await _TPBoardDbContext.SaveChangesAsync();

        return NoContent();
    }
}
