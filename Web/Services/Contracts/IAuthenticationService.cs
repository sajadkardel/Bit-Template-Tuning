using BTT.Shared.Dtos.Account;

namespace BTT.App.Services.Contracts;

public interface IAuthenticationService
{
    Task SignIn(SignInRequestDto dto);

    Task SignOut();
}
