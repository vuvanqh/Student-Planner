using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO;

public class AcademicEventResponse
{
    [Required]
    public FacultyDTO? Faculty { get; set; }
    public EventDetailsOutputDTO? Details { get; set; }

}
