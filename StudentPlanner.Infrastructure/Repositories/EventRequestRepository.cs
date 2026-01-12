using Entities;
using Entities.Events;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;
public class EventRequestRepository: IEventRequestRepository
{
    private readonly ApplicationDbContext _db;

    public EventRequestRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<EventRequest> AddEventRequest(EventRequest request)
    {
        _db.EventRequests.Add(request); 
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task DeleteEventRequest(Guid? requestId)
    {
        _db.EventRequests.Remove(_db.EventRequests.FirstOrDefault(e => e.RequestId == requestId)!);
        await _db.SaveChangesAsync();
    }

    public async Task<List<EventRequest>> GetAllEventRequests()
    {
        return await _db.EventRequests.ToListAsync();
    }

    public async Task<List<EventRequest>> GetEventRequestByFaculty(string? email)
    {
        return await _db.EventRequests.Where(e=>e.FacultyId == email).ToListAsync();
    }

    public async Task<EventRequest?> GetEventRequestById(Guid? id)
    {
        return await _db.EventRequests.FirstOrDefaultAsync(e => e.RequestId == id);
    }

    public async Task<List<EventRequest>> GetEventRequestByUserEmail(string? email)
    {
        return await _db.EventRequests.Where(e => e.User!.Email == email).ToListAsync();
    }
}
