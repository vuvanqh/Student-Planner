using System;
using System.Collections.Generic;
using System.Text;
using ValueObjects;

namespace ServiceContracts.DTO;

public class EventDetailsOutputDTO // for now we validate the eventDetails say in a week we decide not to allow events shorter than 15 minutes => when creating request response we create eventdetails => our old data will be validated under new constraints which may lead to an exception
{
    public string? Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }

    public static EventDetailsOutputDTO? ToEventDetailsDTO(EventDetails? details)
    {

        return details==null?null: new EventDetailsOutputDTO()
        {
            Title = details.Title,
            StartTime = details.StartTime,
            EndTime = details.EndTime,
            Location = details.Location,
            Description = details.Description
        };
    }
}
