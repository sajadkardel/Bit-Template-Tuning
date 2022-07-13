using BTT.Data;
using BTT.Data.Models.TodoItem;
using BTT.Data.Repositories.Contracts;
using BTT.Shared.Dtos.TodoItem;

namespace BTT.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class TodoItemController : ControllerBase
{
    private IMapper _mapper = default!;

    private IRepository<TodoItem> _repository  = default!;

    public TodoItemController(IRepository<TodoItem> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet, EnableQuery]
    public IQueryable<TodoItemDto> Get(CancellationToken cancellationToken)
    {
        return _repository.TableNoTracking
            .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider, cancellationToken);
    }

    [HttpGet("{id:int}")]
    public async Task<TodoItemDto> Get(int id, CancellationToken cancellationToken)
    {
        var todoItem = await Get(cancellationToken).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (todoItem is null)
            throw new ResourceNotFoundException(nameof(ErrorStrings.ToDoItemCouldNotBeFound));

        return todoItem;
    }

    [HttpPost]
    public async Task Post(TodoItemDto dto, CancellationToken cancellationToken)
    {
        var todoItemToAdd = _mapper.Map<TodoItem>(dto);

        todoItemToAdd.UserId = User.GetUserId();

        await _repository.AddAsync(todoItemToAdd, cancellationToken);
    }

    [HttpPut]
    public async Task Put(TodoItemDto dto, CancellationToken cancellationToken)
    {
        var todoItemToUpdate = await _repository.GetByIdAsync(cancellationToken, dto.Id);

        if (todoItemToUpdate is null)
            throw new ResourceNotFoundException(nameof(ErrorStrings.ToDoItemCouldNotBeFound));

        var updatedTodoItem = _mapper.Map(dto, todoItemToUpdate);

        await _repository.UpdateAsync(updatedTodoItem, cancellationToken);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(new TodoItem { Id = id }, cancellationToken);
    }
}

