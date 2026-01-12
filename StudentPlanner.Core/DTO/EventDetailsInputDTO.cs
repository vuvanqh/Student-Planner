using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValueObjects;

namespace StudentPlanner.Core.DTO;

public class EventDetailsInputDTO: IValidatableObject //we should accept invalid data and pass a response always
{
    [Required]
    public Guid? EventId { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    [Required]
    public string? Location { get; set; }
    public string? Description { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Title))
            yield return new ValidationResult("Title is required");

        if (string.IsNullOrWhiteSpace(Location))
            yield return new ValidationResult("Location is required");

        if (EndTime <= StartTime)
            yield return new ValidationResult("EndTime must be after StartTime");
    }
}
