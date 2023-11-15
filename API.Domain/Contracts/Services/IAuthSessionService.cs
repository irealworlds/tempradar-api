using API.Domain.Dto;

namespace API.Domain.Contracts.Services;

public interface IAuthSessionService
{
    public Task Create(AuthSessionCreationDataDto data);
    public Task InvalidateCurrentSession();
}