using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
    // In-memory list to store todo items for simplicity
    private readonly List<TodoItem> _todos =
    [
        new TodoItem { Id = 1, Title = "Buy groceries", IsCompleted = false },
        new TodoItem { Id = 2, Title = "Walk the dog", IsCompleted = true },
        new TodoItem { Id = 3, Title = "Read a book", IsCompleted = false },
        new TodoItem { Id = 4, Title = "Clean the house", IsCompleted = false },
        new TodoItem { Id = 5, Title = "Pay bills", IsCompleted = true },
        new TodoItem { Id = 6, Title = "Water the plants", IsCompleted = false },
    ];

    private int _nextId = 7;

    public List<TodoItem> GetAll() => _todos;

    public TodoItem? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);

    public TodoItem Add(string title)
    {
        var item = new TodoItem { Id = _nextId++, Title = title };
        _todos.Add(item);
        return item;
    }

    public bool ToggleComplete(int id)
    {
        var item = GetById(id);
        if (item is null) return false;
        item.IsCompleted = !item.IsCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var item = GetById(id);
        if (item is null) return false;
        _todos.Remove(item);
        return true;
    }
}
