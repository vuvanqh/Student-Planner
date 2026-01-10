using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Entities.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class User
{
    [Key]
    [StringLength(40)]
    public required string UserEmail { get; set; }
    [StringLength(40)]
    public required string PasswordHash { get; set; }
    [StringLength(20)]
    public required string FirstName { get; set; }
    [StringLength(20)]
    public required string Surname { get; set; }

    [ForeignKey("EventId")]
    public ICollection<PersonalEvent> PersonalEvents { get; set; } = new List<PersonalEvent>();
    [ForeignKey("EventRequestId")]
    public ICollection<EventRequest> EventRequests { get; set; } = new List<EventRequest>();
}
