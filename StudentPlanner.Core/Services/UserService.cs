using System;
using System.Collections.Generic;
using System.Text;
using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
using RepositoryContracts;

namespace Services;

public class UserService : IUserService
{
    private List<User> _users = new List<User>();
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository; 
    }

    public async Task<UserResponse> CreateUser(CreateUserRequest? createUserRequest)
    {
        if (createUserRequest == null) throw new ArgumentNullException();

        User user = createUserRequest.ToUser();
        await _userRepository.AddUser(user);

        return user.ToUserResponse();
    }

    public async Task<List<UserResponse>> GetAllUsers()
    {
        return (await _userRepository.GetAllUsers()).Select(u => u.ToUserResponse()).ToList();
    }

    public async Task<UserResponse?> GetUserByEmail(string? email)
    {
        if(email==null) throw new ArgumentNullException();

        User? user = await _userRepository.GetUserByEmail(email);

        return user==null ? null : user.ToUserResponse();
    }
}
