using Entities;
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

public static class FacultyExtention
{
    public static FacultyDTO ToFacultyDTO(this Faculty faculty)
    {
        return new FacultyDTO()
        {
            FacultyId = faculty.FacultyId,
            Name = faculty.FacultyName,
            DisplayName = faculty.DisplayName
        };
    }
}

