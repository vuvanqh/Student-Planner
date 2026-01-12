using Entities.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class PersonalEventResponse
{
    public Guid EventId { get; set; }
    public EventDetailsOutputDTO Details { get; set; } = null!;
}

public static class PersonalEventExtentions
{
    public static PersonalEventResponse ToPersonalEventResponse(this PersonalEvent response)
    {
        return new PersonalEventResponse()
        {
            EventId = response.EventId,
            Details = EventDetailsOutputDTO.ToEventDetailsDTO(response.Details)!
        };
    }
}
