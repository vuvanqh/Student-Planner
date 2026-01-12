using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class UsosEventResponse
{
    [Required] public string EventId = null!;
    [Required] public EventDetailsOutputDTO EventDetailsOutputDTO { get; set; } = null!;
}

