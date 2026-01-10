using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entities.Events;

public class PersonalEvent: Event
{
    //relationships
    [StringLength(50)]
    public required string Email { get; set; } //UserEmail
    public required User User { get; set; }
    
}
