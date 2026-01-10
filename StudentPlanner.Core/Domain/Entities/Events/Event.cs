using System.ComponentModel.DataAnnotations;
using ValueObjects;
namespace Entities.Events;

/// <summary>
/// Domain Model for Event details
/// </summary>
public abstract class Event
{
    [Key]
    public required Guid EventId { get; set; }
    public required EventDetails Details { get; set; }
}
