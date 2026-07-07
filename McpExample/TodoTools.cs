using System.ComponentModel;
using ModelContextProtocol.Server;

namespace McpExample;

[McpServerToolType]
public sealed class TodoTools(TodoService service)
{
    [McpServerTool(
        Name = "list_todos",
        Title = "List TODO items",
        ReadOnly = true)]
    [Description("Returns all TODO items.")]
    public IReadOnlyList<object> ListTodos()
        => service.ListTodos().Cast<object>().ToList();


    [McpServerTool(
        Name = "search_todos",
        Title = "Search TODOs",
        ReadOnly = true)]
    [Description("Searches TODO items by title text.")]
    public IReadOnlyList<object> SearchTodos([Description("Text to search for in TODO titles.")] string query)
        => service.SearchTodos(query).Cast<object>().ToList();


    [McpServerTool(
        Name = "complete_todo_by_title",
        Title = "Complete TODO by title")]
    [Description("Marks a TODO item as completed using its exact title.")]
    public object? CompleteTodoByTitle([Description("Exact TODO title to complete.")] string title)
        => service.CompleteTodoByTitle(title);
}