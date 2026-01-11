using StudentPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ValueObjects;


namespace Entities.Events;
/// <summary>
/// Domain Model for Event Request details
/// </summary>
public class EventRequest
{
    [Key]
    public required Guid RequestId { get; set; }
    public required RequestType RequestType { get; set; }
    public required RequestStatus RequestStatus { get; set; }
    public required DateTime SubmissionDate { get; set; }
    public Guid? TargetEventId { get; set; } //for delete
    public EventDetails? Details { get; set; } //for create and update

    //relationships
    public required Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }

    [ForeignKey("FacultyId")]
    [StringLength(40)]
    public required string FacultyId { get; set; }
    public Faculty? Faculty { get; set; }
}
