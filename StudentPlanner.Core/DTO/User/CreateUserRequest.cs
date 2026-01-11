using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities;
using StudentPlanner.Core.Domain;

namespace StudentPlanner.Core.DTO;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Email cannot be blank")]
    [EmailAddress(ErrorMessage = "Email is not in a valid eamil format")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    [Compare(nameof(Password),ErrorMessage ="Passwords must match")]
    public string? ConfirmPassword { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }
    
    public ApplicationUser ToUser() => new ApplicationUser()
    {
        Email = Email!,
        FirstName = FirstName!,
        Surname = Surname!
    };
}
