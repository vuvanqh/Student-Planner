using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class LoginResponse
{
    //session
    [Required] public string AccessToken { get; set; } = null!;
    [Required] public DateTime ExpiresAt { get; set; }

    //data
    [Required] public string FirstName { get; set; } = null!;
    [Required] public string Surname { get; set; } = null!;
    [Required,EmailAddress] public string Email { get; set; } = null!;
    public List<PersonalEventResponse>? PersonalEvents { get; set; }
    public List<AcademicEventResponse>? AcademicEvents { get; set; }
    public List<UsosEventResponse>? UsosEvents { get; set; }

}
