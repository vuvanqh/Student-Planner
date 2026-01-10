using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryContracts;

public interface IUserRepository
{
    public Task<User> AddUser(User user);
    public Task<List<User>> GetAllUsers();
    public Task<User?> GetUserByEmail(string email);
}
