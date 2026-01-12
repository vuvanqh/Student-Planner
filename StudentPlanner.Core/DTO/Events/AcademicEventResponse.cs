using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Entities.Events;

namespace StudentPlanner.Core.DTO;

public class AcademicEventResponse
{

    [Required] public Guid EventId { get; set; }
    [Required] public FacultyDTO Faculty { get; set; } = null!;
    [Required] public EventDetailsOutputDTO Details { get; set; } = null!;

}

public static class AcademicEventExtention
{
    public static AcademicEventResponse ToAcademicEventResponse(this AcademicEvent e)
    {
        return new AcademicEventResponse()
        {
            EventId = e.EventId,
            Details = EventDetailsOutputDTO.ToEventDetailsDTO(e.Details)!,
            Faculty = e.Faculty!.ToFacultyDTO()

        };
    }
}