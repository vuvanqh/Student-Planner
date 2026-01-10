using System;
using System.Collections.Generic;
using System.Text;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class UserService : IUserService
{
    private List<User> _users = new List<User>();
    private readonly ApplicationDbContext _studentPlannerDb;
    public UserService(ApplicationDbContext studentPlannerDb)
    {
        _studentPlannerDb = studentPlannerDb; 
    }

    public async Task<UserResponse> CreateUser(CreateUserRequest? createUserRequest)
    {
        if (createUserRequest == null) throw new ArgumentNullException();

        User user = createUserRequest.ToUser();
        _studentPlannerDb.Users.Add(user);
        await _studentPlannerDb.SaveChangesAsync();

        return user.ToUserResponse();
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        return await _studentPlannerDb.Users.Select(u => u.ToUserResponse()).ToListAsync();
    }

    public async Task<UserResponse?> GetUserByEmail(string? email)
    {
        if(email==null) throw new ArgumentNullException();

        User? user = await _studentPlannerDb.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user==null ? null : user.ToUserResponse();
    }
}
