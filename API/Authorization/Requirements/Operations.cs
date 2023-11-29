using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace API.Authorization.Requirements;

public static class Operations
{
    public static OperationAuthorizationRequirement Create = new() { Name = nameof(Create) };
    public static OperationAuthorizationRequirement Read = new() { Name = nameof(Read) };
    public static OperationAuthorizationRequirement Update = new() { Name = nameof(Update) };
    public static OperationAuthorizationRequirement Delete = new() { Name = nameof(Delete) };
}