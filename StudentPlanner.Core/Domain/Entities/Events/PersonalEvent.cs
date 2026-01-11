using StudentPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Events;

public class PersonalEvent: Event
{
    //relationships
    public required Guid UserId { get; set; } 
    public ApplicationUser? User { get; set; }
    
}
