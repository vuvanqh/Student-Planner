using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities;

namespace ServiceContracts.DTO;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Email cannot be blank")]
    [EmailAddress(ErrorMessage = "Email is not in a valid eamil format")]
    public string? Email { get; set; }
    [Required]
    public string? PasswordHash { get; set; }
    [Required]
    [Compare(nameof(PasswordHash),ErrorMessage ="Passwords must match")]
    public string? ConfirmPasswordHash { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }
    
    public User ToUser() => new User()
    {
        Email = Email!,
        PasswordHash = PasswordHash!,
        FirstName = FirstName!,
        Surname = Surname!
    };
}
