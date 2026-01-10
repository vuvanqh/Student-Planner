using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities;

public class UserFacultyAssignment
{
    [StringLength(40)]
    public required string Email { get; set; }
    [StringLength(40)]
    public required string FacultyId { get; set; }

    public User? User { get; set; }
    public Faculty? Faculty { get; set; }
    
}
