using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;
public class LoginInputDTO
{
    [Required(ErrorMessage = "Email cannot be blank")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password cannot be blank")]
    public string Password { get; set; } = null!;
}
