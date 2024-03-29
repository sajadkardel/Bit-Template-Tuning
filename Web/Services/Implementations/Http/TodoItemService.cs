﻿using BTT.Shared.Dtos.TodoItem;
using BTT.Shared.Markers;

namespace BTT.App.Services.Implementations;

public partial class TodoItemService : ITodoItemService, IScopedDependency
{
    [AutoInject] private HttpClient _httpClient = default!;

    public async Task<List<TodoItemDto>> GetTodoItems()
    {
        var todoItem = await _httpClient.GetFromJsonAsync("TodoItem", AppJsonContext.Default.ApiResultListTodoItemDto);

        return todoItem?.Data ?? new();
    }

    public async Task AddTodoItem(TodoItemDto dto)
    {
        await _httpClient.PostAsJsonAsync("TodoItem", dto, AppJsonContext.Default.TodoItemDto);
    }

    public async Task UpdateTodoItem(TodoItemDto dto)
    {
        await _httpClient.PutAsJsonAsync("TodoItem", dto, AppJsonContext.Default.TodoItemDto);
    }

    public async Task DeleteTodoItem(TodoItemDto dto)
    {
        await _httpClient.DeleteAsync($"TodoItem/{dto.Id}");
    }
}
