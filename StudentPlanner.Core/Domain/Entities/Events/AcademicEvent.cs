using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using Entities;

namespace Entities.Events;

//NOTE: required preferred with entities, [Required] recommended for DTO for validation
public class AcademicEvent: Event
{

    //relationships
    [StringLength(40)]
    public required string FacultyId { get; set; }
    public required Faculty Faculty { get; set; }

    //Subscribers => relationships with users i suppose
}
