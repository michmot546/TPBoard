using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;

[ApiController]
[Route("api/[controller]")]
public class TableElementController : Controller
{
    private readonly ITableElementService _tableElementService;

    public TableElementController(ITableElementService tableElementService)
    {
        _tableElementService = tableElementService;
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetAllElements")]
    public IActionResult GetAllElements()
    {
        var elements = _tableElementService.GetAllTableElements();
        return Ok(elements);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetElementById/{id}")]
    public IActionResult GetElementById(int id)
    {
        var element = _tableElementService.GetTableElementById(id);

        if (element == null)
        {
            return NotFound();
        }

        return Ok(element);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpGet("GetElementsByTableId/{tableId}")]
    public IActionResult GetElementsByTableId(int tableId)
    {
        var elements = _tableElementService.GetTableElementsByTableId(tableId);
        return Ok(elements);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpPost("CreateTableElement")]
    public IActionResult CreateElement([FromBody] TableElement newElement)
    {
        if (newElement == null)
        {
            return BadRequest("Invalid table element data");
        }

        _tableElementService.CreateTableElement(newElement);

        return CreatedAtAction(nameof(GetElementById), new { id = newElement.Id }, newElement);
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpPut("UpdateTableElement/{id}")]
    public IActionResult UpdateElement(int id, [FromBody] TableElement updatedElement)
    {
        if (id != updatedElement.Id)
        {
            return BadRequest("Invalid element data");
        }

        _tableElementService.UpdateTableElement(updatedElement);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpDelete("DeleteTableElement/{id}")]
    public IActionResult DeleteElement(int id)
    {
        var element = _tableElementService.GetTableElementById(id);

        if (element == null)
        {
            return NotFound();
        }

        _tableElementService.DeleteTableElement(id);

        return NoContent();
    }

    [Authorize(Roles = "Admin,Moderator,User")]
    [HttpPost("{tableElementId}/assign/{userId}")]
    public IActionResult AssignUserToTableElement(int tableElementId, int userId)
    {
        _tableElementService.AssignUserToTableElement(tableElementId, userId);
        return NoContent();
    }
}
