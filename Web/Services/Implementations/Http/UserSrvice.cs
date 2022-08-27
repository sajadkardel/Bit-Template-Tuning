using BTT.Shared.Dtos.Account;
using BTT.Shared.Marker;

namespace BTT.App.Services.Implementations;

public partial class UserSrvice : IUserSrvice, IScopedDependency
{
    [AutoInject] private HttpClient _httpClient = default!;

    public async Task<UserDto> GetCurrentUser()
    {
        var user = await _httpClient.GetFromJsonAsync("User/GetCurrentUser", AppJsonContext.Default.ApiResultUserDto);

        return user?.Data ?? new();
    }

    public async Task UpdateProfile(UserDto dto)
    {
        await _httpClient.PutAsJsonAsync("User", dto, AppJsonContext.Default.EditUserDto);
    }
}
