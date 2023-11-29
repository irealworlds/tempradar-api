using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Authorization.Requirements;

public static class Operations
{
    public static OperationAuthorizationRequirement Create = new() { Name = nameof(Operations.Create) };
    public static OperationAuthorizationRequirement Read = new() { Name = nameof(Operations.Read) };
    public static OperationAuthorizationRequirement Update = new() { Name = nameof(Operations.Update) };
    public static OperationAuthorizationRequirement Delete = new() { Name = nameof(Operations.Delete) };
}