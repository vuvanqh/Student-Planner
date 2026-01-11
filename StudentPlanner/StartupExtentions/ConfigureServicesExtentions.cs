using Entities;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudentPlanner.UI;

public static class ConfigureServicesExtention
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventRequestService, EventRequestService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRequestRepository, EventRequestRepository>();

        services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(config.GetConnectionString("Default")); });

        services.AddIdentity<ApplicationUser, ApplicationRole>() //create role & user tables with these properties
                .AddEntityFrameworkStores<ApplicationDbContext>() //store them in this dbcontext
                .AddDefaultTokenProviders() //for run-time token generation
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>() //repository for user
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); //repository for roles
        
    }
}
