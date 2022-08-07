using BTT.Shared.Dtos.Account;

namespace BTT.App.Services.Contracts;

public interface IAuthenticationService
{
    Task SignUp(SignUpRequestDto dto);

    Task SendConfirmEmail(SignUpRequestDto dto);

    Task SignIn(SignInRequestDto dto);

    Task SignOut();

    Task SendResetPasswordEmail(SendResetPasswordEmailRequestDto dto);
}
