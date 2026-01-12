using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class UpdateEventRequest
{
    [Required] public Guid RequestId {  get; set; }
    [Required] public string RequestStatus { get; set; } = null!;
}
