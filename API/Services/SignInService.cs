using API.Application.Contracts;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace API.Services;

public class SignInService : SignInManager<ApplicationUser>, ISignInService
{
    public SignInService(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory,
        optionsAccessor, logger, schemes, confirmation)
    {
    }
}