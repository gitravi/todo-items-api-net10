namespace TodoApi.Models;

// Represents a single to-do item with an ID, title, and completion status.
public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public TodoItem(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = false;
    }
}