using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class LoginOutputDTO
{
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string Surname { get; set; } = null!;
    [Required,EmailAddress] public string Email { get; set; } = null!;
    public List<PersonalEventResponse>? PersonalEvents { get; set; }
    public List<AcademicEventResponse>? AcademicEvents { get; set; }
}
