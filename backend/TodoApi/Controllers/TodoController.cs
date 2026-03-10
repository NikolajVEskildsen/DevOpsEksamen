using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(TodoService todoService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll() => todoService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetById(int id)
    {
        var item = todoService.GetById(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<TodoItem> Create([FromBody] string title)
    {
        var item = todoService.Add(title);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPatch("{id}/toggle")]
    public IActionResult Toggle(int id) =>
        todoService.ToggleComplete(id) ? NoContent() : NotFound();

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) =>
        todoService.Delete(id) ? NoContent() : NotFound();
}
