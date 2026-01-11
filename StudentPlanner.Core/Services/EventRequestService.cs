using ServiceContracts;
using ServiceContracts.DTO;

using Entities.Events;
using Entities;
using RepositoryContracts;


namespace Services;

public class EventRequestService : IEventRequestService
{
    private readonly List<EventRequest> _eventRequests = new List<EventRequest>();
    private readonly IEventRequestRepository _eventRequestRepository;

    public EventRequestService(IEventRequestRepository eventRequestRepository )
    {
        _eventRequestRepository = eventRequestRepository;
     }
    public async Task<EventRequestResponse> CreateEventRequest(CreateEventRequest? request)
    {
        if(request==null || request.Details==null || request.UserId==null)
            throw new ArgumentNullException(nameof(request));


        EventRequest eventRequest = request.ToEventRequest();

        await _eventRequestRepository.AddEventRequest(eventRequest);
        return eventRequest.ToEventRequestResponse();
    }

    public async Task<List<EventRequestResponse>> GetAllEventRequests()
    {
        return (await _eventRequestRepository.GetAllEventRequests()).Select(req => req.ToEventRequestResponse()).ToList();
    }

    public async Task<EventRequestResponse?> GetEventRequestById(Guid? eventRequestId)
    {
        if (eventRequestId == null) throw new ArgumentNullException("Request id has to be provided");

        EventRequest? req = await _eventRequestRepository.GetEventRequestById(eventRequestId);
        if (req == null)
            return null;

        return req.ToEventRequestResponse();
    }

    public async Task<List<EventRequestResponse>> GetEventRequestsByManagerId(string? userEmail)
    {
        if (userEmail == null) throw new ArgumentNullException("Manager id has to be provided");

        List<EventRequestResponse> req = (await _eventRequestRepository.GetEventRequestByUserEmail(userEmail))
                                                       .Select(req => req.ToEventRequestResponse())
                                                       .ToList();
        return req;
    }
}
