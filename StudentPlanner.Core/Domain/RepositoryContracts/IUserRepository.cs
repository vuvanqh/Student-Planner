using Entities;
using StudentPlanner.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContracts;

public interface IUserRepository
{
    public Task<ApplicationUser> AddUser(ApplicationUser user);
    public Task<List<ApplicationUser>> GetAllUsers();
    public Task<ApplicationUser?> GetUserByEmail(string email);
}
