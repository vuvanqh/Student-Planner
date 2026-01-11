using Entities;
using Microsoft.AspNetCore.Identity;
using RepositoryContracts;
using ServiceContracts;
using StudentPlanner.Core.DTO;
using StudentPlanner.Core.Domain;
using StudentPlanner.Core.Errors;
using StudentPlanner.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services;

public class UserService : IUserService
{
    private List<ApplicationUser> _users = new List<ApplicationUser>();
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository; 
        _userManager = userManager;
    }

    public async Task<UserResponse> CreateUser(CreateUserRequest? createUserRequest)
    {
        if (createUserRequest == null) throw new ArgumentNullException();

        ApplicationUser user = createUserRequest.ToUser();

        var result = await _userManager.CreateAsync(user, createUserRequest.Password!);
        if(!result.Succeeded)
        { 
            throw new IdentityOperationException(
                result.Errors.Select(e => e.Description)
            );
        }
        await _userManager.AddToRoleAsync(user, UserRoles.User);
        return user.ToUserResponse();
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        return (await _userRepository.GetAllUsers()).Select(u => u.ToUserResponse()).ToList();
    }

    public async Task<UserResponse?> GetUserByEmail(string? email)
    {
        if(email==null) throw new ArgumentNullException();

        ApplicationUser? user = await _userRepository.GetUserByEmail(email);

        return user==null ? null : user.ToUserResponse();
    }
}
