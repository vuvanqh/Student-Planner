using Entities;
using Entities.Events;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Text;
using ValueObjects;

namespace Repositories;

public class AcademicEventRepository : IAcademicEventRepository
{
    private readonly ApplicationDbContext _db;

    public AcademicEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<AcademicEvent> AddAcademicEvent(AcademicEvent academicEvent)
    {
        _db.AcademicEvents.Add(academicEvent);
        await _db.SaveChangesAsync();
        return academicEvent;
    }

    public async Task DeleteAcademicEvent(Guid eventId)
    {
        _db.AcademicEvents.Remove((await _db.AcademicEvents.FirstOrDefaultAsync(e => e.EventId == eventId))!);
        await _db.SaveChangesAsync();
    }

    public async Task<AcademicEvent?> GetAcademicEvent(Guid eventId)
    {
        return await _db.AcademicEvents.FirstOrDefaultAsync(e=>e.EventId== eventId);
    }

    public async Task<List<AcademicEvent>> GetAcademicEvents()
    {
        return await _db.AcademicEvents.ToListAsync();
    }

    public async Task<List<AcademicEvent>> GetAcademicEventsByFaculty(string facultyId)
    {
        return await _db.AcademicEvents.Where(e => e.FacultyId == facultyId).ToListAsync();
    }

    public async Task<AcademicEvent> UpdateAcademicEvent(Guid eventId, EventDetails details)
    {
        AcademicEvent e = (await _db.AcademicEvents.FirstOrDefaultAsync(e=>e.EventId==eventId))!;
        e.Details = details;
        await _db.SaveChangesAsync();
        return e;
    }
}

