using System;
using System.Collections.Generic;
using System.Text;
using StudentPlanner.Core.DTO;

namespace ServiceContracts;

public interface IUserService
{
    /// <summary>
    /// Adds a new user to the list of users
    /// </summary>
    /// <param name="createUserRequest">User data object that is to be added</param>
    /// <returns></returns>
    Task<UserResponse> CreateUser(CreateUserRequest? createUserRequest);

    /// <summary>
    /// Retrieves the list of all users in the system
    /// </summary>
    /// <returns></returns>
    Task<List<UserResponse>> GetAllUsers();

    /// <summary>
    /// Retrieves the user with the given email
    /// </summary>
    /// <param name="email"></param>
    /// <returns>null if user doesn't exist</returns>
    Task<UserResponse?> GetUserByEmail(string? email);
}
