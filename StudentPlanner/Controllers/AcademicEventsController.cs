using Entities;
using Entities.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using StudentPlanner.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPlanner.UI.Controllers;

[Route("academic-events")]
[ApiController]
public class AcademicEventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AcademicEventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all academic events.
    /// </summary>
    /// <remarks>
    /// This endpoint returns the complete list of academic events available
    /// in the system.
    /// </remarks>
    /// <returns>A list of academic events.</returns>
    /// <response code="200">Returns the list of academic events.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<AcademicEventResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AcademicEventResponse>>> GetAcademicEvents()
    {
        return (await _context.AcademicEvents.ToListAsync()).Select(e=>e.ToAcademicEventResponse()).ToList();
    }

    /// <summary>
    /// Retrieves a specific academic event by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the academic event.</param>
    /// <returns>The requested academic event.</returns>
    /// <response code="200">Returns the academic event.</response>
    /// <response code="404">If the academic event does not exist.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AcademicEventResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<AcademicEventResponse>> GetAcademicEvent(Guid id)
    {
        var academicEvent = await _context.AcademicEvents.FindAsync(id);

        if (academicEvent == null)
        {
            return NotFound();
        }

        return academicEvent.ToAcademicEventResponse();
    }

    /// <summary>
    /// Updates an existing academic event.
    /// </summary>
    /// <remarks>
    /// The identifier in the route must match the identifier of the academic
    /// event provided in the request body.
    /// </remarks>
    /// <param name="id">The unique identifier of the academic event.</param>
    /// <param name="academicEvent">The updated academic event data.</param>
    /// <returns>The updated event.</returns>
    /// <response code="200">The academic event was updated successfully.</response>
    /// <response code="400">If the route identifier does not match the payload.</response>
    /// <response code="404">If the academic event does not exist.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(AcademicEventResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PutAcademicEvent(Guid id, AcademicEvent academicEvent)
    {
        if (id != academicEvent.EventId)
        {
            return BadRequest();
        }

        _context.Entry(academicEvent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AcademicEventExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(academicEvent.ToAcademicEventResponse());
    }

    /// <summary>
    /// Creates a new academic event.
    /// </summary>
    /// <param name="academicEvent">The academic event to be created.</param>
    /// <returns>The newly created academic event.</returns>
    /// <response code="201">Returns the created academic event.</response>
    [HttpPost]
    [ProducesResponseType(typeof(AcademicEventResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<AcademicEventResponse>> PostAcademicEvent(AcademicEvent academicEvent)
    {
        _context.AcademicEvents.Add(academicEvent);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAcademicEvent", new { id = academicEvent.EventId }, academicEvent);
    }
    /// <summary>
    /// Deletes an existing academic event.
    /// </summary>
    /// <param name="id">The unique identifier of the academic event.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">The academic event was deleted successfully.</response>
    /// <response code="404">If the academic event does not exist.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAcademicEvent(Guid id)
    {
        var academicEvent = await _context.AcademicEvents.FindAsync(id);
        if (academicEvent == null)
        {
            return NotFound();
        }

        _context.AcademicEvents.Remove(academicEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Subscribes the authenticated user to an academic event.
    /// </summary>
    /// <param name="eventId">Identifier of the academic event.</param>
    /// <response code="204">Subscription created.</response>
    /// <response code="403">If the user is not authorized.</response>
    /// <response code="404">If the event does not exist.</response>
    [Authorize(Roles = "User")]
    [HttpPut("{eventId}/subscribe")]
    public async Task<IActionResult> SubscribeToEvent(Guid eventId)
    {
        return NoContent();
    }

    /// <summary>
    /// Unsubscribes the authenticated user from an academic event.
    /// </summary>
    /// <param name="eventId">Identifier of the academic event.</param>
    /// <response code="204">Subscription removed.</response>
    /// <response code="403">If the user is not authorized.</response>
    /// <response code="404">If the event or subscription does not exist.</response>
    [Authorize(Roles = "User")]
    [HttpPut("{eventId}/unsubscribe")]
    public async Task<IActionResult> UnubscribeToEvent(Guid eventId)
    {
        return NoContent();
    }

    private bool AcademicEventExists(Guid id)
    {
        return _context.AcademicEvents.Any(e => e.EventId == id);
    }
}
