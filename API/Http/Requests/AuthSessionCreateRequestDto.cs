using System.ComponentModel.DataAnnotations;
using API.Domain.Dto;

namespace API.Http.Requests;

public class AuthSessionCreateRequestDto : AuthSessionCreationDataDto
{
    [Required]
    [EmailAddress]
    public override required string Email { get; set; }
    
    [Required]
    public override required string Password { get; set; }
}