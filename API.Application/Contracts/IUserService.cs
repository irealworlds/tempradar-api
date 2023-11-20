using System.Security.Claims;
using API.Domain.Entities;

namespace API.Application.Contracts;

public interface IUserService
{
    /// <summary>
    /// Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
    /// the principal or null.
    /// </summary>
    /// <param name="principal">The principal which contains the user id claim.</param>
    /// <returns>The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
    /// the principal or null</returns>
    public Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal principal);
}