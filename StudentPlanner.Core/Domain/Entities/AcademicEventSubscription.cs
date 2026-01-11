using Entities.Events;
using StudentPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace Entities;

public class AcademicEventSubscription
{
    [StringLength(40)]
    public required string FacultyId { get; set; }
    public required Guid UserId { get; set; }
    public Faculty? Faculty { get; set; }
    public ApplicationUser? User { get; set; }
}
