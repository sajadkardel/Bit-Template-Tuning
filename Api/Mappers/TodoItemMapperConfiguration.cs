using BTT.Api.Models.TodoItem;
using BTT.Shared.Dtos.TodoItem;

namespace BTT.Api.Mappers;

public class TodoItemMapperConfiguration : Profile
{
    public TodoItemMapperConfiguration()
    {
        CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}
