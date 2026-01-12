using Entities;
using Entities.Events;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using StudentPlanner.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using ValueObjects;

namespace Repositories;

public class PersonalEventRepository : IPersonalEventRepository
{
    private readonly ApplicationDbContext _db;

    public PersonalEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PersonalEvent> AddPersonalEvent(Guid userId, PersonalEvent personalEvent)
    {
        _db.PersonalEvents.Add(new PersonalEvent() { EventId = Guid.NewGuid() ,Details = personalEvent.Details, UserId = userId });
        await _db.SaveChangesAsync();
        return personalEvent;
    }

    public async Task DeletePersonalEvent(Guid eventId)
    {
        _db.PersonalEvents.Remove((await _db.PersonalEvents.FirstOrDefaultAsync(e => e.EventId == eventId))!);
        await _db.SaveChangesAsync();
    }

    public async Task<PersonalEvent?> GetPersonalEvent(Guid eventId)
    {
        return await _db.PersonalEvents.FirstOrDefaultAsync(e=>e.EventId == eventId);
    }

    public async Task<List<PersonalEvent>> GetPersonalEvents(Guid userId)
    {
        return await _db.PersonalEvents.Where(e=>e.UserId== userId).ToListAsync();
    }

    public async Task<PersonalEvent> UpdatePersonalEvent(Guid eventId, EventDetails details)
    {
        PersonalEvent e = _db.PersonalEvents.FirstOrDefault(e=>e.EventId == eventId)!;
        e.Details = details;
        await _db.SaveChangesAsync();
        return e;
    }
}
