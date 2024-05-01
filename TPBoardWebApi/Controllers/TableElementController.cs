using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TableElementController : Controller
{
    private readonly TPBoardDbContext _TPBoardDbContext;

    public TableElementController(TPBoardDbContext TPBoardDbContext)
    {
        _TPBoardDbContext = TPBoardDbContext;
    }

    [HttpGet("GetAllElements")]
    public IActionResult GetAllElements()
    {
        var elements = _TPBoardDbContext.TableElements.ToList();
        return Ok(elements);
    }

    [HttpGet("GetElementById/{id}")]
    public IActionResult GetElementById(int id)
    {
        var element = _TPBoardDbContext.TableElements.Find(id);

        if (element == null)
        {
            return NotFound();
        }

        return Ok(element);
    }

    [HttpPost("CreateElement")]
    public async Task<IActionResult> CreateElement([FromBody] TableElement newElement)
    {
        if (newElement == null)
        {
            return BadRequest("Invalid element data");
        }

        _TPBoardDbContext.TableElements.Add(newElement);
        await _TPBoardDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetElementById), new { id = newElement.Id }, newElement);
    }

    [HttpPut("UpdateElement/{id}")]
    public async Task<IActionResult> UpdateElement(int id, [FromBody] TableElement updatedElement)
    {
        if (id != updatedElement.Id)
        {
            return BadRequest("Invalid element data");
        }

        _TPBoardDbContext.Entry(updatedElement).State = EntityState.Modified;

        try
        {
            await _TPBoardDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_TPBoardDbContext.TableElements.Any(e => e.Id == id))
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

    [HttpDelete("DeleteElement/{id}")]
    public async Task<IActionResult> DeleteElement(int id)
    {
        var element = await _TPBoardDbContext.TableElements.FindAsync(id);

        if (element == null)
        {
            return NotFound();
        }

        _TPBoardDbContext.TableElements.Remove(element);
        await _TPBoardDbContext.SaveChangesAsync();

        return NoContent();
    }
}
