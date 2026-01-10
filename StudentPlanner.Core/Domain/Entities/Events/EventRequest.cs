using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
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
    public required string UserEmail { get; set; }
    public User? User { get; set; }
}
