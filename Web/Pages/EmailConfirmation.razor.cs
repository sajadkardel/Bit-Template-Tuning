﻿namespace BTT.App.Pages;

public partial class EmailConfirmation
{
    [AutoInject] private IAuthenticationService authService = default!;

    [AutoInject] private NavigationManager navigationManager = default!;

    [Parameter]
    [SupplyParameterFromQuery]
    public string? Email { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "email-confirmed")]
    public bool EmailConfirmed { get; set; }

    public bool IsLoading { get; set; }

    public BitMessageBarType EmailConfirmationMessageType { get; set; }
    public string? EmailConfirmationMessage { get; set; }

    private void RedirectToSignIn()
    {
        navigationManager.NavigateTo("/sign-in");
    }

    private async Task ResendLink()
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;
        EmailConfirmationMessage = null;

        try
        {
            await authService.SendConfirmEmail(new()
            {
                Email = Email,
            });

            EmailConfirmationMessageType = BitMessageBarType.Success;

            EmailConfirmationMessage = "The confirmation link has been re-sent to your email address.";
        }
        catch (KnownException e)
        {
            EmailConfirmationMessageType = BitMessageBarType.Error;

            EmailConfirmationMessage = ErrorStrings.ResourceManager.Translate(e.Message, Email!);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
