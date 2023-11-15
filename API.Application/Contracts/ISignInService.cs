using Microsoft.AspNetCore.Identity;

namespace API.Application.Contracts;

public interface ISignInService
{
    public Task<SignInResult> PasswordSignInAsync(
        string userName,
        string password,
        bool isPersistent,
        bool lockoutOnFailure);

    public Task<SignInResult> TwoFactorAuthenticatorSignInAsync(
        string code,
        bool isPersistent,
        bool rememberClient);

    public Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(
        string recoveryCode);

    public Task SignOutAsync();
}