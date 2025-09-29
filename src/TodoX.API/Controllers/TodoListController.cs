using Microsoft.AspNetCore.Mvc;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/todolists")]
public class TodoListController : ControllerBase
{
    [HttpGet]
    public IActionResult GetTodos()
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

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        return NoContent();
    }
}