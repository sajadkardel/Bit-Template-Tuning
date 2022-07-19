using BTT.Api.Infrastructure;
using BTT.Data.Models.Account;
using BTT.Shared.Dtos.Account;

namespace BTT.Api.Controllers;

public partial class UserController : BaseController
{
    [AutoInject] private UserManager<User> _userManager = default!;

    [AutoInject] private IMapper _mapper = default!;

    [HttpGet("[action]")]
    public async Task<UserDto> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

        if (user is null)
            throw new ResourceNotFoundException();

        return _mapper.Map<User, UserDto>(user);
    }

    [HttpPut]
    public async Task<IActionResult> Update(EditUserDto userDto, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        if (userId != User.GetUserId())
            throw new UnauthorizedException();

        var user = await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

        if (user is null)
            throw new ResourceNotFoundException();

        var updatedUser = _mapper.Map(userDto, user);

        await _userManager.UpdateAsync(updatedUser);

        return Ok();
    }
}
