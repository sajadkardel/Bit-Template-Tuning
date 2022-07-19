using AutoMapper;
using BTT.Data.Models.TodoItem;
using BTT.Shared.Dtos.TodoItem;

namespace BTT.Data.Mappers;

public class TodoItemMapperConfiguration : Profile
{
    public TodoItemMapperConfiguration()
    {
        CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
}
