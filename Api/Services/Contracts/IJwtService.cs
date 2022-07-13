using BTT.Data.Models.Account;
using BTT.Shared.Dtos.Account;

namespace BTT.Api.Services.Contracts;

public interface IJwtService
{
    Task<SignInResponseDto> GenerateToken(User user);
}
