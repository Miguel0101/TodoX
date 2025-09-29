using Microsoft.AspNetCore.Mvc;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/todolists/{listId}/items")]
public class TodoItemController : ControllerBase
{
    [HttpGet]
    public IActionResult GetItems()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create()
    {
        return Created();
    }

    [HttpPut("{id}")]
    public IActionResult Edit(Guid id)
    {
        return Ok();
    }

    [HttpPatch("{id}/complete")]
    public IActionResult Complete(Guid id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        return NoContent();
    }
}