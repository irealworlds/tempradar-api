namespace API.Domain.Dto;

public interface IIdentityCreationDataDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}