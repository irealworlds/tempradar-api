using System.Security.Claims;
using API.Domain.Contracts.Services;
using API.Domain.Dto;
using API.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
{
    public async Task<IdentityDto?> GetIdentityByClaimsPrincipal(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);

        if (user == null)
        {
            return null;
        }

        return new IdentityDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}