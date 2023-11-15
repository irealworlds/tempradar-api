using API.Application.Contracts;
using API.Domain.Contracts.Services;
using API.Domain.Dto;

namespace API.Application.Services;

public class AuthSessionService(ISignInService signInService) : IAuthSessionService
{
    public async Task Create(AuthSessionCreationDataDto data)
    {
        const bool isPersistent = true;
        
        var result = await signInService.PasswordSignInAsync(
            data.Email,
            data.Password,
            isPersistent,
            lockoutOnFailure: true
        );
        
        if (result.RequiresTwoFactor)
        {
            if (!string.IsNullOrEmpty(data.TwoFactorCode))
            {
                result = await signInService.TwoFactorAuthenticatorSignInAsync(
                    data.TwoFactorCode,
                    isPersistent,
                    rememberClient: isPersistent
                );
            }
            else if (!string.IsNullOrEmpty(data.TwoFactorRecoveryCode))
            {
                result = await signInService.TwoFactorRecoveryCodeSignInAsync(
                    data.TwoFactorRecoveryCode
                );
            }
        }

        if (!result.Succeeded)
        {
            throw new Exception("Could not perform authentication");
        }
        
        // The signInManager already produced the needed response in the form of a cookie or bearer token.
        return;
    }

    public async Task InvalidateCurrentSession()
    {
        await signInService.SignOutAsync();
    }
}