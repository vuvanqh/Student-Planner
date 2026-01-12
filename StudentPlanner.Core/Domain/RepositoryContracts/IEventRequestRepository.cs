using Entities.Events;

namespace RepositoryContracts;

public interface IEventRequestRepository
{
    public Task<EventRequest> AddEventRequest(EventRequest request);
    public Task<List<EventRequest>> GetAllEventRequests();
    public Task<EventRequest?> GetEventRequestById(Guid? id);
    public Task<List<EventRequest>> GetEventRequestByUserEmail(string? email);
    public Task<List<EventRequest>> GetEventRequestByFaculty(string? email);
    public Task DeleteEventRequest(Guid? eventId);
}
