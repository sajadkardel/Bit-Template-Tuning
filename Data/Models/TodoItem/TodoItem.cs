using BTT.Data.Models.Account;
using BTT.Data.Models.Common;

namespace BTT.Data.Models.TodoItem;

public class TodoItem : BaseEntity
{
    public string? Title { get; set; }
    public DateTimeOffset Date { get; set; }
    public bool IsDone { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
    public int UserId { get; set; }
}
