using API.Authorization.Requirements;
using API.Domain.Contracts.Services;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Authorization.Handlers
{
    public class PinnedSensorCrudHandler(IPinnedSensorService pinnedSensorService) : AuthorizationHandler<OperationAuthorizationRequirement, PinnedSensor>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
            PinnedSensor resource)
        {
            if (requirement.Name == Operations.Read.Name)
            {
                if (await pinnedSensorService.UserCanReadSensorAsync(context.User, resource))
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement.Name == Operations.Update.Name)
            {
                if (await pinnedSensorService.UserCanUpdateSensorAsync(context.User, resource))
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement.Name == Operations.Delete.Name)
            {
                if (await pinnedSensorService.UserCanDeleteSensorAsync(context.User, resource))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, $"Operation {requirement.Name} not handled for resource of type {nameof(PinnedSensor)}."));
            }
        }
    }
}
