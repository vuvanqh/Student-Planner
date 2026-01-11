using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Entities;
using Entities.Events;
using StudentPlanner.Core.Domain;

namespace ServiceContracts.DTO;

public class UserResponse
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? Surname { get; set; }

    public List<FacultyDTO>? Faculties{ get; set; }
    public List<AcademicEventResponse>? AcademicEvents { get; set; }
    public List<PersonalEventResponse>? PersonalEvents { get; set; }

    /// <summary>
    /// Compares current user object data with the parameter object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(UserResponse)) return false;
        UserResponse resp = (UserResponse)obj;
        return this.Email == resp.Email;
    }
    public override int GetHashCode() => base.GetHashCode();
    
}

public static class UserExtention
{
    public static UserResponse ToUserResponse(this ApplicationUser user) 
    {
        return new UserResponse()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            Surname = user.Surname,
        };
    }
}