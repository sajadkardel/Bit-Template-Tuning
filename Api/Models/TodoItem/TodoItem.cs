using BTT.Api.Models.Account;
using BTT.Api.Models.Common;

namespace BTT.Api.Models.TodoItem;

public class TodoItem : IEntity
{
    public string? Title { get; set; }
    public DateTimeOffset Date { get; set; }
    public bool IsDone { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public int UserId { get; set; }
}
