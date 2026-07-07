namespace McpExample;

public record TodoItem(Guid Id, string Title, bool Completed);

public class TodoService
{
    private readonly List<TodoItem> _todos =
    [
        new(Guid.NewGuid(), "Buy milk", false),
        new(Guid.NewGuid(), "Learn MCP", true),
        new(Guid.NewGuid(), "Write API documentation", false)
    ];

    public IReadOnlyList<TodoItem> ListTodos() => _todos;

    public IReadOnlyList<TodoItem> SearchTodos(string query) =>
        [.. _todos
            .Where(x =>x.Title.Contains(query,StringComparison.OrdinalIgnoreCase))
        ];

    public TodoItem? CompleteTodoByTitle(string title)
    {
        var index = _todos.FindIndex(x => x.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (index < 0)
            return null;

        var completed = _todos[index] with { Completed = true };

        _todos[index] = completed;

        return completed;
    }
}