using BTT.Shared.Dtos.Account;

namespace BTT.App.Pages;

public partial class ForgotPassword
{
    [AutoInject] private IAuthenticationService authService = default!;

    public SendResetPasswordEmailRequestDto ForgotPasswordModel { get; set; } = new();

    public bool IsLoading { get; set; }

    public BitMessageBarType ForgotPasswordMessageType { get; set; }

    public string? ForgotPasswordMessage { get; set; }

    private bool IsSubmitButtonEnabled =>
        ForgotPasswordModel.Email.HasValue()
        && IsLoading is false;

    private async Task Submit()
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;
        ForgotPasswordMessage = null;

        try
        {
            await authService.SendResetPasswordEmail(ForgotPasswordModel);

            ForgotPasswordMessageType = BitMessageBarType.Success;

            ForgotPasswordMessage = "The reset password link has been sent.";
        }
        catch (KnownException e)
        {
            ForgotPasswordMessageType = BitMessageBarType.Error;

            ForgotPasswordMessage = ErrorStrings.ResourceManager.Translate(e.Message, ForgotPasswordModel.Email!);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
