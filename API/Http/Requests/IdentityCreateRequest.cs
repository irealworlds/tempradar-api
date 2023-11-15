using System.ComponentModel.DataAnnotations;
using API.Domain.Dto;

namespace API.Http.Requests;

public class IdentityCreateRequest : IIdentityCreationDataDto
{
    [Required]
    public string FirstName { get; set; } = String.Empty;
    
    [Required]
    public string LastName { get; set; } = String.Empty;
    
    [Required]
    public string Password { get; set; } = String.Empty;
    
    [Required]
    public string Email { get; set; } = String.Empty;
}