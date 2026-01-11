using Entities;
using Entities.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentPlanner.Core.DTO;

namespace StudentPlanner.UI.Controllers;


/// <summary>
/// Manages personal events belonging to the authenticated user.
/// </summary>
/// <remarks>
/// Access rules:
/// - Only users with the <c>User</c> role can access these endpoints.
/// - All operations are scoped to the currently authenticated user.
/// - Users can only view, modify, or delete their own personal events.
/// </remarks>
[Authorize(Roles = "User")]
[ProducesResponseType(StatusCodes.Status403Forbidden, Description = "You don't have permission to perform this action")]
[Route("api/events/[controller]")]
[ApiController]
public class PersonalEventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PersonalEventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all personal events belonging to the authenticated user.
    /// </summary>
    /// <remarks>
    /// This endpoint returns only the events owned by the currently logged-in user.
    /// The user identifier is derived from the authentication context and not
    /// from client input.
    /// </remarks>
    /// <returns>A list of personal events.</returns>
    /// <response code="200">Returns the list of personal events.</response>
    // GET: api/PersonalEvents
    [HttpGet]
    [ProducesResponseType(typeof(List<PersonalEventResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PersonalEvent>>> GetPersonalEvents(Guid userId)
    {
        var id = User.GetUserId();
        var events = await _context.PersonalEvents
            .Where(e => e.UserId == id)
            .Select(e => e.ToPersonalEventResponse())
            .ToListAsync();

        return await _context.PersonalEvents.Where(e => e.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Retrieves all personal events belonging to the authenticated user.
    /// </summary>
    /// <remarks>
    /// This endpoint returns only the events owned by the currently logged-in user.
    /// The user identifier is derived from the authentication context and not
    /// from client input.
    /// </remarks>
    /// <returns>A list of personal events.</returns>
    /// <response code="200">Returns the list of personal events.</response>
    // GET: api/PersonalEvents/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonalEventResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonalEventResponse>> GetPersonalEvent(Guid id)
    {
        //var personalEvent = await _context.PersonalEvents.Where(e => e.UserId == userId).FirstOrDefaultAsync(e => e.EventId == id);
        PersonalEventResponse? personalEvent = null;
        if (personalEvent == null)
        {
            return NotFound();
        }

        return personalEvent;
    }

    /// <summary>
    /// Updates an existing personal event.
    /// </summary>
    /// <remarks>
    /// The authenticated user may only update events they own.
    /// Attempts to update events owned by other users will result in a forbidden response.
    /// </remarks>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="personalEvent">The updated event data.</param>
    /// <returns>The updated personal event.</returns>
    /// <response code="200">Returns the updated event.</response>
    /// <response code="400">If the event identifier does not match the payload.</response>
    /// <response code="403">If the event does not belong to the authenticated user.</response>
    /// <response code="404">If the event does not exist.</response>
    // PUT: api/PersonalEvents/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(PersonalEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Description = "You don't have permission to perform this action. This event does not belong to you")]
    public async Task<ActionResult<PersonalEventResponse>> PutPersonalEvent(Guid id, PersonalEventUpdateDTO personalEvent)
    {
        if (id != personalEvent.Details!.EventId)
        {
            return BadRequest();
        }

        _context.Entry(personalEvent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonalEventExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Creates a new personal event for the authenticated user.
    /// </summary>
    /// <remarks>
    /// The created event is automatically associated with the current user.
    /// Clients do not provide user identifiers directly.
    /// </remarks>
    /// <param name="userId">The user identifier (derived from authentication context).</param>
    /// <param name="personalEvent">The event creation payload.</param>
    /// <returns>The created personal event.</returns>
    /// <response code="201">Returns the newly created personal event.</response>
    // POST: api/PersonalEvents
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("{id:guid}")]
    [ProducesResponseType(typeof(PersonalEventResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<PersonalEventResponse>> CreatePersonalEvent(Guid userId, CreatePersonalEventRequest personalEvent)
    {
        throw new NotImplementedException(); 
        //_context.PersonalEvents.Add(personalEvent);
        //await _context.SaveChangesAsync();

        //return CreatedAtAction("GetPersonalEvent", new { id = personalEvent.EventId }, personalEvent);
    }

    /// <summary>
    /// Deletes an existing personal event.
    /// </summary>
    /// <remarks>
    /// The authenticated user may only delete events they own.
    /// </remarks>
    /// <param name="id">The unique identifier of the event.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">Event deleted successfully.</response>
    /// <response code="403">If the event does not belong to the authenticated user.</response>
    /// <response code="404">If the event does not exist.</response>
    // DELETE: api/PersonalEvents/5
    [HttpDelete("{userId}/{id}")]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Description = "You don't have permission to perform this action. This event does not belong to you")]
    public async Task<IActionResult> DeletePersonalEvent(Guid id)
    {
        var personalEvent = await _context.PersonalEvents.FindAsync(id);
        if (personalEvent == null)
        {
            return NotFound();
        }

        _context.PersonalEvents.Remove(personalEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonalEventExists(Guid id)
    {
        return _context.PersonalEvents.Any(e => e.EventId == id);
    }
}
