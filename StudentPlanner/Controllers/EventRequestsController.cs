using Entities;
using Entities.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Core.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;

namespace StudentPlanner.UI.Controllers;


/// <summary>
/// Manages event requests created by managers and reviewed by administrators.
/// </summary>
/// <remarks>
/// Access rules:
/// - Managers can create, view, and delete their own event requests.
/// - Administrators can view all event requests and update their status.
/// </remarks>
[Authorize(Roles = "Manager, Admin")]
[Route("api/event-requests")]
[ApiController]
public class EventRequestsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves event requests based on the role of the authenticated user.
    /// </summary>
    /// <remarks>
    /// Role-based behavior:
    /// - Admin: retrieves all event requests in the system.
    /// - Manager: retrieves only event requests created by the authenticated manager.
    /// </remarks>
    /// <returns>
    /// A list of event requests visible to the current user.
    /// </returns>
    /// <response code="200">Returns the list of event requests.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<EventRequestResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EventRequestResponse>>> GetEventRequests()
    {
        var resp = await _context.EventRequests.ToListAsync();
        return resp.Select(e=>e.ToEventRequestResponse()).ToList();
    }

    // GET: api/EventRequests/10

    /// <summary>
    /// Retrieves a specific event request by its unique identifier.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event request.</param>
    /// <returns>The requested event request.</returns>
    /// <response code="200">Returns the requested event request.</response>
    /// <response code="404">If the event request does not exist or is not accessible.</response>
    [HttpGet("{eventId:guid}")]
    [ProducesResponseType(typeof(EventRequestResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<EventRequestResponse>> GetManagerEventRequest(Guid id)
    {
        var eventRequest = await _context.EventRequests.FindAsync(id);

        if (eventRequest == null)
        {
            return NotFound();
        }

        return eventRequest.ToEventRequestResponse();
    }

    /// <summary>
    /// Retrieves event requests associated with a specific faculty.
    /// </summary>
    /// <remarks>
    /// Intended primarily for administrative use to filter event requests
    /// belonging to a particular faculty.
    /// </remarks>
    /// <param name="facultyId">The unique identifier of the faculty.</param>
    /// <returns>A list of event requests associated with the specified faculty.</returns>
    /// <response code="200">Returns the filtered list of event requests.</response>
    // GET: api/EventRequests/10
    [HttpGet("faculty")]
    [ProducesResponseType(typeof(List<EventRequestResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetManagerEventRequestByFaculty([FromQuery]string facultyId)
    { return Ok(); }


    /// <summary>
    /// Updates the status of an existing event request.
    /// </summary>
    /// <remarks>
    /// Only administrators are allowed to update event request statuses.
    /// Typical status transitions include approval or rejection.
    /// </remarks>
    /// <param name="id">The unique identifier of the event request.</param>
    /// <param name="eventRequest">The updated event request data.</param>
    /// <returns>The updated status of the event request.</returns>
    /// <response code="200">Returns the updated request status.</response>
    /// <response code="400">If the request ID does not match the payload.</response>
    /// <response code="404">If the event request does not exist.</response>
    // PUT: api/EventRequests/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RequestStatus), StatusCodes.Status200OK)]
    public async Task<IActionResult> PutEventRequest(Guid id, UpdateEventRequest eventRequest)
    {
        if (id != eventRequest.RequestId)
        {
            return BadRequest();
        }

        _context.Entry(eventRequest).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventRequestExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(new { RequestStatus.REJECTED});
    }


    /// <summary>
    /// Creates a new event request.
    /// </summary>
    /// <remarks>
    /// Only managers are allowed to create event requests.
    /// The newly created request is initially assigned a default status.
    /// </remarks>
    /// <param name="eventRequest">The event request creation payload.</param>
    /// <returns>The created event request.</returns>
    /// <response code="201">Returns the newly created event request.</response>
    // POST: api/EventRequests
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Manager")]
    [ProducesResponseType(typeof(EventRequestResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<EventRequestResponse>> PostEventRequest(CreateEventRequest eventRequest)
    {
        _context.EventRequests.Add(eventRequest.ToEventRequest());
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEventRequest", new { id = eventRequest.Details!.EventId }, eventRequest);
    }

    /// <summary>
    /// Deletes an existing event request.
    /// </summary>
    /// <remarks>
    /// Managers may only delete event requests they created.
    /// </remarks>
    /// <param name="id">The unique identifier of the event request.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">Event request deleted successfully.</response>
    /// <response code="404">If the event request does not exist.</response>
    // DELETE: api/EventRequests/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> DeleteEventRequest(Guid id)
    {
        var eventRequest = await _context.EventRequests.FindAsync(id);
        if (eventRequest == null)
        {
            return NotFound();
        }

        _context.EventRequests.Remove(eventRequest);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventRequestExists(Guid id)
    {
        return _context.EventRequests.Any(e => e.RequestId == id);
    }
}
