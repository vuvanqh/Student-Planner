using Entities;
using Entities.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;
using StudentPlanner.Core.Domain;
using StudentPlanner.Core.ValueObjects;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentPlanner.UI;

public static class ConfigureServicesExtention
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        //services.AddProblemDetails();

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesAttribute("application/json")); //we only return json objects
            options.Filters.Add(new ConsumesAttribute("application/json")); //and accept json only objects too
           // options.Filters.Add<IdentityExceptionFilter>();
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventRequestService, EventRequestService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRequestRepository, EventRequestRepository>();

        services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(config.GetConnectionString("Default")); });

        services.AddIdentity<ApplicationUser, ApplicationRole>( options =>
        {
            options.Password.RequiredLength = 10;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 5;
        }) //create role & user tables with these properties
                
                .AddEntityFrameworkStores<ApplicationDbContext>() //store them in this dbcontext
                .AddDefaultTokenProviders() //for run-time token generation
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>() //repository for user
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); //repository for roles

        services.AddAuthorization(options =>
        {
            //options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); // enforce authorization policy for all action methods
            options.AddPolicy("CanCreateEventRequest", policy =>
            {
                policy.RequireRole("Manager");
            });
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( options =>
        {
            options.OperationFilter<DefaultResponseFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,"StudentPlanner.UI.xml"));
        });
    }

    public static async Task InitializeDb(this IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();

            foreach (var role in UserRoles.All)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new ApplicationRole { Name = role });

                    if (!result.Succeeded)
                    {
                        throw new Exception(string.Join(", ",
                            result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }

        using (var scope = service.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());

            if (!db.Users.Any())
            {
                string users = System.IO.File.ReadAllText("seedData/users.json");
                List<ApplicationUser> usr = JsonSerializer.Deserialize<List<ApplicationUser>>(users, options)!;
                foreach (ApplicationUser u in usr)
                    await userManager.CreateAsync(u, "Password123!");
            }
            if (!db.Faculties.Any())
            {
                string faculties = System.IO.File.ReadAllText("seedData/faculties.json");
                db.Faculties.AddRange(JsonSerializer.Deserialize<List<Faculty>>(faculties, options)!);

                await db.SaveChangesAsync();
            }
            if (!db.UserFacultyAssignments.Any())
            {
                string userFacultyAssignments = System.IO.File.ReadAllText("seedData/userFacultyAssignments.json");
                db.UserFacultyAssignments.AddRange(JsonSerializer.Deserialize<List<UserFacultyAssignment>>(userFacultyAssignments, options)!);

                await db.SaveChangesAsync();
            }
            if (!db.AcademicEvents.Any())
            {
                string academicEvents = System.IO.File.ReadAllText("seedData/academicEvents.json");
                db.AcademicEvents.AddRange(JsonSerializer.Deserialize<List<AcademicEvent>>(academicEvents, options)!);

                await db.SaveChangesAsync();
            }
            if (!db.PersonalEvents.Any())
            {
                string personalEvents = System.IO.File.ReadAllText("seedData/personalEvents.json");
                db.PersonalEvents.AddRange(JsonSerializer.Deserialize<List<PersonalEvent>>(personalEvents, options)!);

                await db.SaveChangesAsync();
            }
            if (!db.EventRequests.Any())
            {
                string eventRequests = System.IO.File.ReadAllText("seedData/eventRequests.json");
                db.EventRequests.AddRange(JsonSerializer.Deserialize<List<EventRequest>>(eventRequests, options)!);

                await db.SaveChangesAsync();
            }
        }
    }
}



public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (id == null)
            throw new InvalidOperationException("User ID claim is missing.");

        return Guid.Parse(id);
    }
}

