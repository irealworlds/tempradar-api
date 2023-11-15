using System.ComponentModel.DataAnnotations;
using API.Domain.Dto;

namespace API.Http.Requests;

public class IdentityCreateRequest : IdentityCreationDataDto
{
    [Required]
    public new string FirstName { get; set; } = String.Empty;
    
    [Required]
    public new string LastName { get; set; } = String.Empty;
    
    [Required]
    [EmailAddress]
    public new string Email { get; set; } = String.Empty;
    
    [Required]
    public new string Password { get; set; } = String.Empty;
}