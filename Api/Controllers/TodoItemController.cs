using BTT.Api.Infrastructure;
using BTT.Data.Models.TodoItem;
using BTT.Data.Repositories.Contracts;
using BTT.Shared.Dtos.TodoItem;

namespace BTT.Api.Controllers;

public partial class TodoItemController : BaseController
{
    [AutoInject] private IMapper _mapper = default!;

    [AutoInject] private IRepository<TodoItem> _repository  = default!;

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
    public async Task<IActionResult> Post(TodoItemDto dto, CancellationToken cancellationToken)
    {
        var todoItemToAdd = _mapper.Map<TodoItem>(dto);

        todoItemToAdd.UserId = User.GetUserId();

        await _repository.AddAsync(todoItemToAdd, cancellationToken);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(TodoItemDto dto, CancellationToken cancellationToken)
    {
        var todoItemToUpdate = await _repository.GetByIdAsync(cancellationToken, dto.Id);

        if (todoItemToUpdate is null)
            throw new ResourceNotFoundException(nameof(ErrorStrings.ToDoItemCouldNotBeFound));

        var updatedTodoItem = _mapper.Map(dto, todoItemToUpdate);

        await _repository.UpdateAsync(updatedTodoItem, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(new TodoItem { Id = id }, cancellationToken);

        return Ok();
    }
}

