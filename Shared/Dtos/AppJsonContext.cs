using BTT.Shared.Dtos.Account;
using BTT.Shared.Dtos.TodoItem;
using BTT.Shared.Infrastructure;

namespace BTT.Shared.Dtos;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]

[JsonSerializable(typeof(TodoItemDto))]
[JsonSerializable(typeof(ApiResult<TodoItemDto>))]
[JsonSerializable(typeof(ApiResult<List<TodoItemDto>>))]

[JsonSerializable(typeof(UserDto))]
[JsonSerializable(typeof(ApiResult<UserDto>))]
[JsonSerializable(typeof(ApiResult<List<UserDto>>))]

[JsonSerializable(typeof(SignInRequestDto))]
[JsonSerializable(typeof(ApiResult<SignInRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<SignInRequestDto>>))]

[JsonSerializable(typeof(SignInResponseDto))]
[JsonSerializable(typeof(ApiResult<SignInResponseDto>))]
[JsonSerializable(typeof(ApiResult<List<SignInResponseDto>>))]

[JsonSerializable(typeof(SignUpRequestDto))]
[JsonSerializable(typeof(ApiResult<SignUpRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<SignUpRequestDto>>))]

[JsonSerializable(typeof(EditUserDto))]
[JsonSerializable(typeof(ApiResult<EditUserDto>))]
[JsonSerializable(typeof(ApiResult<List<EditUserDto>>))]

[JsonSerializable(typeof(RestExceptionPayload))]
[JsonSerializable(typeof(ApiResult<RestExceptionPayload>))]
[JsonSerializable(typeof(ApiResult<List<RestExceptionPayload>>))]

[JsonSerializable(typeof(EmailConfirmedRequestDto))]
[JsonSerializable(typeof(ApiResult<EmailConfirmedRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<EmailConfirmedRequestDto>>))]

[JsonSerializable(typeof(SendConfirmationEmailRequestDto))]
[JsonSerializable(typeof(ApiResult<SendConfirmationEmailRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<SendConfirmationEmailRequestDto>>))]

[JsonSerializable(typeof(SendResetPasswordEmailRequestDto))]
[JsonSerializable(typeof(ApiResult<SendResetPasswordEmailRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<SendResetPasswordEmailRequestDto>>))]

[JsonSerializable(typeof(ResetPasswordRequestDto))]
[JsonSerializable(typeof(ApiResult<ResetPasswordRequestDto>))]
[JsonSerializable(typeof(ApiResult<List<ResetPasswordRequestDto>>))]

public partial class AppJsonContext : JsonSerializerContext
{
}