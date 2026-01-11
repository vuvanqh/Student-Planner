using ValueObjects;
using Entities.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO;


public class CreateEventRequest
{
    //public DateTime SubmissionDate { get; set; } client does not get to choose when to the date its done automatically when we save it
    public EventDetailsInputDTO? Details { get; set; }
    public Guid? UserId { get; set; }
    public string? FacultyId { get; set; }
    public EventRequest ToEventRequest()
    {
        return new EventRequest()
        {
            RequestId = Guid.NewGuid(),
            RequestType = RequestType.CREATE,
            RequestStatus = RequestStatus.PENDING,
            SubmissionDate = DateTime.Now,
            Details = new EventDetails(Details!.Title!, Details.StartTime, Details.EndTime, Details.Location!, Details.Description),
            UserId = (Guid)UserId!,
            FacultyId = FacultyId!
        };
    }
}