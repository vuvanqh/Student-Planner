using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ServiceContracts.DTO;

public class PersonalEventResponse
{
    public EventDetailsOutputDTO? Details { get; set; }

}
