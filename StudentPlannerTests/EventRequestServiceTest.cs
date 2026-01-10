using Entities.Events;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using ValueObjects;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using EntityFrameworkCoreMock;
using AutoFixture;

namespace StudentSchedulerTests;
public class EventRequestServiceTest
{
    private readonly IEventRequestService _eventRequestService;
    private readonly Fixture fixture = new Fixture();
    public EventRequestServiceTest()
    {
        var eventRequestsInitialData = new List<EventRequest>() { };
        //1. Mock dbContext
        DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(new DbContextOptionsBuilder<ApplicationDbContext>().Options);
        
        //2. Mock dbSets
        ApplicationDbContext dbContext = dbContextMock.Object;
        dbContextMock.CreateDbSetMock(temp => temp.EventRequests);

        _eventRequestService = new EventRequestService(dbContext);
    }

    #region CreateEventRequest 
    [Fact]
    public async Task CreateEventRequest_NullRequest()
    {
        //Arrange
        CreateEventRequest? request = null;

        //Assert

        //Fluent Assertion
        //Func<Task> action = async () => await _eventRequestService.CreateEventRequest(request);
        //action.Should().ThrowAsync<ArgumentNullException>();

        await Assert.ThrowsAsync<ArgumentNullException>(async ()=> await _eventRequestService.CreateEventRequest(request)); //Act
    }

    [Fact]
    public async Task CreateEventRequest_ManagerIdIsNull()
    {
        //Arrange
        CreateEventRequest request = new CreateEventRequest() { UserEmail=null};
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async() => await _eventRequestService.CreateEventRequest(request)); //Act
    }
    [Fact]
    public async Task  CreateEventRequest_ProperResponse()
    {
        //Arrange
        CreateEventRequest request = fixture.Create<CreateEventRequest>();
        EventRequestResponse response = await _eventRequestService.CreateEventRequest(request);
        //Assert
        Assert.True(response.RequestId!=Guid.Empty); //Act
        Assert.Contains(response, await _eventRequestService.GetAllEventRequests()); //Assert.Contains internally calls Equals method
    }
    #endregion

    #region GetAllEventRequests
    [Fact]
    public async Task GetAllEventRequests_EmptyList()
    {
        List<EventRequestResponse> response_list = await _eventRequestService.GetAllEventRequests();
        Assert.Empty(response_list);
    }
    [Fact]
    public async Task GetAllEventRequests_ProperResponse()
    {
        //Arrange
        List<CreateEventRequest> data = new List<CreateEventRequest>();
        fixture.AddManyTo<CreateEventRequest>(data);
       
        //Act
        List<EventRequestResponse> resp = new List<EventRequestResponse>();

        foreach (CreateEventRequest d in data)
            resp.Add(await _eventRequestService.CreateEventRequest(d));
        
        List<EventRequestResponse> responses = await _eventRequestService.GetAllEventRequests();
        foreach (EventRequestResponse response in responses)
            Assert.Contains(response, resp);
    }
    #endregion

    #region GetEventRequestById
    [Fact]
    public async Task GetEventRequestById_NullRequestId()
    {
        //Arrange
        Guid? requestId = null;

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async() => await _eventRequestService.GetEventRequestById(requestId)); //Act
    }

    [Fact]
    public async Task  GetEventRequestById_ValidId()
    {
        //Arrange
        CreateEventRequest? create_request = fixture.Create<CreateEventRequest>();
        EventRequestResponse? response = await _eventRequestService.CreateEventRequest(create_request);

        //Act
        EventRequestResponse? resp = await _eventRequestService.GetEventRequestById(response.RequestId);
        //Assert
        Assert.Equal(resp, response);
    }
    #endregion

    #region GetEventRequestsByManagerId
    [Fact]
    public async Task  GetEventRequestsByManagerId_NullManagerId()
    {
        //Arrange
        Guid? managerId = null;
        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async() => await _eventRequestService.GetEventRequestsByManagerId(managerId)); //Act
    }

    [Fact]
    public async Task GetEventRequestsByManagerId_ValidIdWithNonEmptyList()
    {
        //Arrange
        string managerId = "email@gmail.com";
        List<CreateEventRequest> data = new List<CreateEventRequest>();
        fixture.AddManyTo<CreateEventRequest>(data);

        //Act
        List<EventRequestResponse> actual_response = new List<EventRequestResponse>();

        foreach (CreateEventRequest d in data)
            actual_response.Add(await _eventRequestService.CreateEventRequest(d));

        actual_response = actual_response.Where(resp => resp.UserEmail==managerId).ToList();

        List<EventRequestResponse> response = await _eventRequestService.GetEventRequestsByManagerId(userEmail);
        //Assert
        foreach (EventRequestResponse resp in response)
            Assert.Contains(resp, actual_response);
    }

    [Fact]
    public async Task GetEventRequestsByManagerId_ValidIdWithNoRequests()
    {
        //Arrange
        Guid ManagerId = Guid.NewGuid();
        //Act
        List<EventRequestResponse>? resp = await _eventRequestService.GetEventRequestsByManagerId(ManagerId);
        //Assert
        Assert.Empty(resp);
    }
    #endregion
}
