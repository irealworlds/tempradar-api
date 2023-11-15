namespace API.Domain.Dto;

public class AuthSessionCreationDataDto
{
    public virtual required string Email { get; set; }
    public virtual required string Password { get; set; }
    public virtual string? TwoFactorCode { get; set; }
    public virtual string? TwoFactorRecoveryCode { get; set; }
}