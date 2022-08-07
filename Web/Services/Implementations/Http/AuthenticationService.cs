using BTT.Shared.Dtos.Account;
using System.Net.Http;

namespace BTT.App.Services.Implementations;

public partial class AuthenticationService : IAuthenticationService
{
    [AutoInject] private HttpClient _httpClient = default!;

    [AutoInject] private IJSRuntime _jsRuntime = default!;

    [AutoInject] private AppAuthenticationStateProvider _authenticationStateProvider = default!;

    public async Task SignUp(SignUpRequestDto dto)
    {
        await _httpClient.PostAsJsonAsync("Auth/SignUp", dto, AppJsonContext.Default.SignUpRequestDto);
    }

    public async Task SendConfirmEmail(SignUpRequestDto dto)
    {
        await _httpClient.PostAsJsonAsync("Auth/SendConfirmationEmail", new()
        {
            Email = dto.Email
        }, AppJsonContext.Default.SendConfirmationEmailRequestDto);
    }

    public async Task SignIn(SignInRequestDto dto)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/SignIn", dto, AppJsonContext.Default.SignInRequestDto);

        var result = await response.Content.ReadFromJsonAsync(AppJsonContext.Default.ApiResultSignInResponseDto);

        await _jsRuntime.InvokeVoidAsync("App.setCookie", "access_token", result!.Data.AccessToken, result.Data.ExpiresIn);

        await _authenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }

    public async Task SignOut()
    {
        await _jsRuntime.InvokeVoidAsync("App.removeCookie", "access_token");

        await _authenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }

    public async Task SendResetPasswordEmail(SendResetPasswordEmailRequestDto dto)
    {
        await _httpClient.PostAsJsonAsync("Auth/SendResetPasswordEmail", dto, AppJsonContext.Default.SendResetPasswordEmailRequestDto);
    }
}
