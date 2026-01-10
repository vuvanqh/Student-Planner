
using System.Text.Json;
using Entities;
using Entities.Events;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace StudentPlanner;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IEventRequestService, EventRequestService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IEventRequestRepository, EventRequestRepository>();

        builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!db.AcademicEvents.Any())
            {
                string academicEvents = System.IO.File.ReadAllText("seedData/academicEvents.json");
                db.AcademicEvents.AddRange(JsonSerializer.Deserialize<List<AcademicEvent>>(academicEvents)!);
            }
            if (!db.PersonalEvents.Any())
            {
                string personalEvents = System.IO.File.ReadAllText("seedData/personalEvents.json");
                db.PersonalEvents.AddRange(JsonSerializer.Deserialize<List<PersonalEvent>>(personalEvents)!);
            }
            if (!db.EventRequests.Any())
            {
                string eventRequests = System.IO.File.ReadAllText("seedData/eventRequests.json");
                db.EventRequests.AddRange(JsonSerializer.Deserialize<List<EventRequest>>(eventRequests)!);
            }
            if (!db.Faculties.Any())
            {
                string faculties = System.IO.File.ReadAllText("seedData/faculties.json");
                db.Faculties.AddRange(JsonSerializer.Deserialize<List<Faculty>>(faculties)!);
            }
            if (!db.Users.Any())
            {
                string users = System.IO.File.ReadAllText("seedData/users.json");
                db.Users.AddRange(JsonSerializer.Deserialize<List<User>>(users)!);
            }
            if (!db.UserFacultyAssignments.Any())
            {
                string userFacultyAssignments = System.IO.File.ReadAllText("seedData/userFacultyAssignments.json");
                db.UserFacultyAssignments.AddRange(JsonSerializer.Deserialize<List<UserFacultyAssignment>>(userFacultyAssignments)!);
            }
            db.SaveChanges();
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        app.UseStaticFiles();
        app.Run();
    }
}
