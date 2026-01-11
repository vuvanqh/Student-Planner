using Entities.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class PersonalEventResponse
{
    public EventDetailsOutputDTO? Details { get; set; }
}

public static class PersonalEventExtentions
{
    public static PersonalEventResponse ToPersonalEventResponse(this PersonalEvent response)
    {
        return new PersonalEventResponse()
        {
            Details = EventDetailsOutputDTO.ToEventDetailsDTO(response.Details)
        };
    }
}
