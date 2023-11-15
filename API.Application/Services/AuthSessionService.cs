using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Application.Services;

public class AuthSessionService(SignInManager<ApplicationUser> signInManager) : IAuthSessionService
{
    public async Task Create(AuthSessionCreationDataDto data)
    {
        const bool isPersistent = true;
        signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;
        
        var result = await signInManager.PasswordSignInAsync(
            data.Email,
            data.Password,
            isPersistent,
            lockoutOnFailure: true
        );
        
        if (result.RequiresTwoFactor)
        {
            if (!string.IsNullOrEmpty(data.TwoFactorCode))
            {
                result = await signInManager.TwoFactorAuthenticatorSignInAsync(
                    data.TwoFactorCode,
                    isPersistent,
                    rememberClient: isPersistent
                );
            }
            else if (!string.IsNullOrEmpty(data.TwoFactorRecoveryCode))
            {
                result = await signInManager.TwoFactorRecoveryCodeSignInAsync(
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
        await signInManager.SignOutAsync();
    }
}