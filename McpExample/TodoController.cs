using Microsoft.AspNetCore.Mvc;

namespace McpExample;


[ApiController]
[Route("api/todos")]
public class TodoController(TodoService service)
    : ControllerBase
{
    [HttpGet]
    public IActionResult List() => Ok(service.ListTodos());


    [HttpGet("search")]
    public IActionResult Search([FromQuery] string query) => Ok(service.SearchTodos(query));


    [HttpPost("complete")]
    public IActionResult Complete([FromQuery] string title) =>
        service.CompleteTodoByTitle(title) is { } result
            ? Ok(result)
            : NotFound();
}