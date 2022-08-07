using BTT.Shared.Dtos.TodoItem;

namespace BTT.App.Services.Contracts;

public interface ITodoItemService
{
    Task<List<TodoItemDto>> GetTodoItems();
    Task AddTodoItem(TodoItemDto dto);
    Task UpdateTodoItem(TodoItemDto dto);
    Task DeleteTodoItem(TodoItemDto dto);
}
