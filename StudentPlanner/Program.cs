
using Entities;
using Entities.Events;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;
using StudentPlanner.Core.Domain;
using StudentPlanner.Core.Errors;
using StudentPlanner.Core.ValueObjects;
using StudentPlanner.UI;
using System.Text.Json;
using System.Text.Json.Serialization;
using ValueObjects;

namespace StudentPlanner;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureServices(builder.Configuration);
    
        var app = builder.Build();

        app.UseExceptionHandler(appError =>
        {
           

        });
 

        await app.Services.InitializeDb();

        if (!app.Environment.IsDevelopment())
            app.UseHsts(); //enforces https

        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c=>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentPlanner API v1");
        });

        app.UseRouting(); //matches http to controller

        app.UseAuthentication(); //Reads Identity Cookie & identifies the current working user
        app.UseAuthorization(); //Validates access permissions of the user

        app.MapControllers(); //executes the controller

        app.UseStaticFiles();
        app.Run();
    }
}
