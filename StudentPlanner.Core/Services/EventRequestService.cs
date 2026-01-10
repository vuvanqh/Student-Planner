using ServiceContracts;
using ServiceContracts.DTO;

using Entities.Events;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class EventRequestService : IEventRequestService
{
    private readonly List<EventRequest> _eventRequests = new List<EventRequest>();
    private readonly ApplicationDbContext _studentPlannerDb;
    public EventRequestService(ApplicationDbContext studentPlannerDb)
    {
        _studentPlannerDb = studentPlannerDb;
     }
    public async Task<EventRequestResponse> CreateEventRequest(CreateEventRequest? request)
    {
        if(request==null || request.Details==null || request.ManagerId==Guid.Empty)
            throw new ArgumentNullException(nameof(request));


        EventRequest eventRequest = request.ToEventRequest();

        _studentPlannerDb.EventRequests.Add(eventRequest);
        await _studentPlannerDb.SaveChangesAsync();

        return eventRequest.ToEventRequestResponse();
    }

    public async Task<List<EventRequestResponse>> GetAllEventRequests()
    {
        return await _studentPlannerDb.EventRequests.Select(req => req.ToEventRequestResponse()).ToListAsync();
    }

    public async Task<EventRequestResponse?> GetEventRequestById(Guid? eventRequestId)
    {
        if (eventRequestId == null) throw new ArgumentNullException("Request id has to be provided");

        EventRequest? req = await _studentPlannerDb.EventRequests.FirstOrDefaultAsync(req => req.RequestId == eventRequestId);
        if (req == null)
            return null;

        return req.ToEventRequestResponse();
    }

    public async Task<List<EventRequestResponse>> GetEventRequestsByManagerId(Guid? managerId)
    {
        if (managerId == null) throw new ArgumentNullException("Manager id has to be provided");

        List<EventRequestResponse> req = await _studentPlannerDb.EventRequests.Where(req => req.RequestId == managerId)
                                                       .Select(req => req.ToEventRequestResponse())
                                                       .ToListAsync();
        return req;
    }
}
