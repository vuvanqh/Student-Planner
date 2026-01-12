using StudentPlanner.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Events;
using ValueObjects;

namespace RepositoryContracts;

public interface IAcademicEventRepository
{
    public Task<List<AcademicEvent>> GetAcademicEvents();
    public Task<List<AcademicEvent>> GetAcademicEventsByFaculty(string facultyId);
    public Task<AcademicEvent?> GetAcademicEvent(Guid eventId);
    public Task<AcademicEvent> AddAcademicEvent(AcademicEvent academicEvent);
    public Task<AcademicEvent> UpdateAcademicEvent(Guid eventId, EventDetails details);
    public Task DeleteAcademicEvent(Guid eventId);
}
