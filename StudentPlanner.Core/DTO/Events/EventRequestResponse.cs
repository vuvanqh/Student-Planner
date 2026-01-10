using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValueObjects;
using Entities.Events;

namespace ServiceContracts.DTO;

public class EventRequestResponse //
{
    public Guid RequestId { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public DateTime SubmissionDate { get; set; }
    public Guid? TargetEventId {get;set;}
    public EventDetailsOutputDTO? Details { get; set; }
    public string? ManagerEmail { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(EventRequestResponse)) return false;

        EventRequestResponse resp = (EventRequestResponse)obj;
        
        return resp.RequestId == this.RequestId;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public static class EventRequestExtention
{
    public static EventRequestResponse ToEventRequestResponse(this EventRequest request)
    {
        return new EventRequestResponse()
        {
            RequestId = request.RequestId,
            RequestType = request.RequestType,
            RequestStatus = request.RequestStatus,
            SubmissionDate = request.SubmissionDate,
            Details = EventDetailsOutputDTO.ToEventDetailsDTO(request.Details),
            ManagerEmail = request.UserEmail!,
            TargetEventId = request.TargetEventId,
        };
    }
}
