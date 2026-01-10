using System;
using System.Collections.Generic;
using System.Text;
using Entities.Events;
namespace Entities;

public class AcademicEventSubscription
{
    public required string FacultyId { get; set; }
    public required string Email { get; set; }
    public required Faculty Faculty { get; set; }
    public required User User { get; set; }
}
