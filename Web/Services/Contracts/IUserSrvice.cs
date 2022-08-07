using BTT.Shared.Dtos.Account;

namespace BTT.App.Services.Contracts;

public interface IUserSrvice
{
    Task<UserDto> GetCurrentUser();
    Task UpdateProfile(UserDto dto);
}
