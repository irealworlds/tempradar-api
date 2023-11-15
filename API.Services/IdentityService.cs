using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class IdentityService(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore) : IIdentityService
{
    // Validate the email address using DataAnnotations like the UserValidator does when RequireUniqueEmail = true.
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    private static IdentityDto MapUserToDto(ApplicationUser user)
    {
        return new IdentityDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? String.Empty
        };
    }
    
    public async Task<IdentityDto?> GetIdentityByClaimsPrincipal(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);

        if (user == null)
        {
            return null;
        }

        return MapUserToDto(user);
    }

    public async Task<IdentityDto> CreateIdentity(IIdentityCreationDataDto data)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"A user store with email support is required.");
        }
        
        var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
        var email = data.Email;

        if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
        {
            throw new ArgumentException(userManager.ErrorDescriber.InvalidEmail(email).Description, nameof(data));
        }

        var user = new ApplicationUser();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        user.FirstName = data.FirstName;
        user.LastName = data.LastName;
        var result = await userManager.CreateAsync(user, data.Password);

        if (!result.Succeeded)
        {
            throw new ArgumentException(result.Errors.ToString(), nameof(data));
        }

        // TODO
        // await SendConfirmationEmailAsync(user, userManager, context, email);

        return MapUserToDto(user);
    }
}