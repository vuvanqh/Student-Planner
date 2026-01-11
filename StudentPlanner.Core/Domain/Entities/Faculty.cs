using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Entities.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Faculty
{
    [Key]
    [StringLength(40)]
    public required string FacultyId { get; set; } //Id or code like 1120 or sum but definitely unique
    [StringLength(50)]
    public required string FacultyName {  get; set; } //e.g., Faculty of Mathematics and Information Science\
    [StringLength(10)]
    public required string DisplayName { get; set; } //e.g, MiNI

    //relationships
    public ICollection<AcademicEvent> AcademicEvents { get; set; } = new List<AcademicEvent>(); //NOTE FOR FUTURE SELF: remember about .Include("Property we want to load induced from the relationship")!!!!
    public ICollection<EventRequest> EventRequests { get; set; } = new List<EventRequest>();
}
