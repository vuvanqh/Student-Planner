using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentPlanner.Core.DTO;

public class FacultyDTO
{
    [Required]
    public string? FacultyId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? DisplayName { get; set; }
}
