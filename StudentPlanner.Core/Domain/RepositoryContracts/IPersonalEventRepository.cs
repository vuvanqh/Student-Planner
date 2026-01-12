using Entities.Events;
using StudentPlanner.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using ValueObjects;

namespace RepositoryContracts;

public interface IPersonalEventRepository
{
    public Task<List<PersonalEvent>> GetPersonalEvents(Guid userId);
    public Task<PersonalEvent?> GetPersonalEvent(Guid eventId);
    public Task<PersonalEvent> AddPersonalEvent(Guid userId, PersonalEvent personal);
    public Task<PersonalEvent> UpdatePersonalEvent(Guid eventId, EventDetails details);
    public Task DeletePersonalEvent(Guid eventId);
}
