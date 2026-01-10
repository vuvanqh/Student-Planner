using System;
using System.Collections.Generic;
using System.Text;
using Entities.Events;
namespace Entities;

public class AcademicEventSubscription
{
    public required string FacultyId { get; set; }
    public required string UserEmail { get; set; }
    public Faculty? Faculty { get; set; }
    public User? User { get; set; }
}
