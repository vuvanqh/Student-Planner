using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class CreatePersonalEventRequest
{
    public EventDetailsInputDTO Details { get; set; } = null!;
}
