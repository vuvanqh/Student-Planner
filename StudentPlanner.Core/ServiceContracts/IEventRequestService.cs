using Entities.Events;
using ValueObjects;
using StudentPlanner.Core.DTO;

namespace ServiceContracts;

/// <summary>
/// Represents business logic for manipulating EventRequest entity
/// </summary>
public interface IEventRequestService
{
    /// <summary>
    /// Adds an event request object to the event request list
    /// </summary>
    /// <param name="request">Event Request object that is to be added</param>
    /// <returns>The event request object that was added</returns>
    Task<EventRequestResponse> CreateEventRequest(CreateEventRequest? request);

    /// <summary>
    /// Returns the list of all event requests
    /// </summary>
    /// <returns></returns>
    Task<List<EventRequestResponse>> GetAllEventRequests();

    /// <summary>
    /// Returns an event request object based on the given request id 
    /// </summary>
    /// <param name="eventRequestId">Event Request id to search</param>
    /// <returns>Matching event request as EventRequestResponse object</returns>
    Task<EventRequestResponse?> GetEventRequestById(Guid? eventRequestId);

    /// <summary>
    /// Returns the list of all event requests submitted by the manager of the given manager id
    /// </summary>
    /// <param name="managerId">Manager id whose event requests are to be searched</param>
    /// <returns>List of all event requests submitted by the manager of the given managerId</returns>
    Task<List<EventRequestResponse>> GetEventRequestsByManagerId(string? userEmail);

    Task<List<EventRequestResponse>> GetEventRequestsByFaculty(string? facultyId);

    Task UpdateEventRequestStatus(UpdateEventRequest updateEventRequest);
}
