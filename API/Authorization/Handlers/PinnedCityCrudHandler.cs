using API.Authorization.Requirements;
using API.Domain.Contracts.Services;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Authorization.Handlers;

public class PinnedCityCrudHandler(IPinnedCityService pinnedCityService) : AuthorizationHandler<OperationAuthorizationRequirement, PinnedCity>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
        PinnedCity resource)
    {
        if (requirement.Name == Operations.Read.Name)
        {
            if (await pinnedCityService.UserCanReadCityAsync(context.User, resource))
            {
                context.Succeed(requirement);
            }
        }
        else if (requirement.Name == Operations.Update.Name)
        {
            if (await pinnedCityService.UserCanUpdateCityAsync(context.User, resource))
            {
                context.Succeed(requirement);
            }
        }
        else if (requirement.Name == Operations.Delete.Name)
        {
            if (await pinnedCityService.UserCanDeleteCityAsync(context.User, resource))
            {
                context.Succeed(requirement);
            }
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(this, $"Operation {requirement.Name} not handled for resource of type {nameof(PinnedCity)}."));
        }
    }
}