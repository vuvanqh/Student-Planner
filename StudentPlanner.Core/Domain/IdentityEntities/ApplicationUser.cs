using Entities.Events;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentPlanner.Core.Domain;
public class ApplicationUser: IdentityUser<Guid> //has id, emai, username(login name), emailconfirmed, passwordhash, phone number
{
    [StringLength(20)]
    public required string FirstName { get; set; }
    [StringLength(20)]
    public required string Surname { get; set; }

    //relationships
    public ICollection<PersonalEvent> PersonalEvents { get; set; } = new List<PersonalEvent>();
    public ICollection<EventRequest> EventRequests { get; set; } = new List<EventRequest>();
}
