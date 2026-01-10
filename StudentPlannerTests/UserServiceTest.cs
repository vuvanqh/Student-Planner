using AutoFixture;
using Entities;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Xunit.Abstractions;
namespace StudentSchedulerTests;

public class UserServiceTest
{
    private readonly IUserService _userService;
    private readonly ITestOutputHelper _outputHelper;
    private readonly Fixture fixture = new Fixture();

    public UserServiceTest(ITestOutputHelper helper)
    {
        DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);

        ApplicationDbContext dbContext = dbContextMock.Object;
        dbContextMock.CreateDbSetMock(temp => temp.Users);

        _userService = new UserService(dbContext);
        _outputHelper = helper;
    }

    [Fact]
    public async Task CreateUser_NullArguments()
    {
        //Arrange
        CreateUserRequest? request = null;
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await _userService.CreateUser(request)); //Act
    }

    [Fact]
    public async Task CreateUser_ValidUser()
    {
        //Arrange
        CreateUserRequest request = fixture.Build<CreateUserRequest>().With(usr => usr.Email,"email@gmail.com").Create();

        //Act
        UserResponse resp = await _userService.CreateUser(request);
        List<UserResponse> userResponses = await _userService.GetAllUsers();

        //Assert
        Assert.True(resp.Email == request.Email);
        Assert.Contains(resp, userResponses);
    }


    [Fact]
    public async Task GetUserByEmail_NullEmail()
    {
        //Arrange
        string? email = null;
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await _userService.GetUserByEmail(email));
    }
    [Fact]
    public async Task GetUserByEmail_UserDoesntExist()
    {
        //Arrange
        string? email = "no_dummy@gmail.com";
        CreateUserRequest request = fixture.Build<CreateUserRequest>().With(usr => usr.Email, email).Create();
        UserResponse true_user = await _userService.CreateUser(request);

        //Act
        UserResponse? test_response = await _userService.GetUserByEmail(email);

        //Assert
        Assert.True(test_response == null);
    }

    [Fact]
    public async Task GetUserByEmail_ValidEmail()
    {
        //Arrange
        string? email = "dummy6@gmail.com";
        CreateUserRequest request = fixture.Build<CreateUserRequest>().With(usr => usr.Email, email).Create();
        UserResponse true_user = await _userService.CreateUser(request);

        //Act
        UserResponse? test_response = await _userService.GetUserByEmail(email);

        //Assert
        Assert.Equal(true_user, test_response);
    }
}
