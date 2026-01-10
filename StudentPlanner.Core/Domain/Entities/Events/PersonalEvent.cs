using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entities.Events;

public class PersonalEvent: Event
{
    //relationships
    [StringLength(40)]
    public required string UserEmail { get; set; } //UserEmail
    public User? User { get; set; }
    
}
