using System.Security.Claims;
using API.Domain.Dto;

namespace API.Domain.Contracts.Services;

public interface IIdentityService
{
    public Task<IdentityDto?> GetIdentityByClaimsPrincipal(ClaimsPrincipal principal);
    public Task<IdentityDto> CreateIdentity(IdentityCreationDataDto data);
}