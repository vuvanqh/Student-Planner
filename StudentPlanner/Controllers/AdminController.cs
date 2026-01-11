using Entities;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.Core.DTO;
using StudentPlanner.Core.Domain;
using StudentPlanner.Core.Errors;
using System.Collections.Generic;

namespace StudentPlanner.UI.Controllers;


/// <summary>
/// Provides administrative operations for managing users and roles.
/// </summary>
/// <remarks>
/// Access rules:
/// - Only users with the <c>Admin</c> role may access these endpoints.
/// - These endpoints allow administrators to manage manager accounts.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Creates a new manager account.
    /// </summary>
    /// <remarks>
    /// This endpoint creates a new user account and assigns the Manager role.
    /// Only administrators are authorized to perform this operation.
    /// </remarks>
    /// <param name="userRequest">The manager account creation details.</param>
    /// <returns>The result of the manager creation operation.</returns>
    /// <response code="200">Manager account created successfully.</response>
    /// <response code="400">If the manager already exists.</response>
    [HttpPost("create-manager")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Manager already exists")]
    public async Task<IActionResult> CreateManager(CreateUserRequest userRequest)
    {
        ApplicationUser manager = new ApplicationUser()
        {
            Email = userRequest.Email,
            FirstName = "Ben",
            Surname = "Ten",
        };

        IdentityResult result = await _userManager.CreateAsync(manager, userRequest.Password!);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(manager, UserRoleOptions.Manager.ToString());
            return Ok(result);
        }
        else
        {
            throw new IdentityOperationException(
                result.Errors.Select(e => e.Description)
            );
        }
    }


    /// <summary>
    /// Deletes an existing manager account.
    /// </summary>
    /// <remarks>
    /// Only administrators are allowed to delete manager accounts.
    /// </remarks>
    /// <param name="ManagerEmail">The email address of the manager to delete.</param>
    /// <returns>No content if deletion is successful.</returns>
    /// <response code="204">Manager deleted successfully.</response>
    /// <response code="400">If the email address is not provided.</response>
    /// <response code="404">If the manager does not exist.</response>
    [HttpDelete("delete-manager")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "Manager doesn't exist")]
    public async Task<IActionResult> DeleteManager(string? ManagerEmail)
    {
        if (ManagerEmail == null)
        {
            return BadRequest("Email required");
        }
        //ApplicationUser manager = new ApplicationUser()
        //{
        //    Email = userRequest.Email,
        //    FirstName = "Ben",
        //    Surname = "Ten",
        //};

        //IdentityResult result = await _userManager.CreateAsync(manager, userRequest.Password!);

        //if (result.Succeeded)
        //{
        //    await _userManager.AddToRoleAsync(manager, UserRoleOptions.Manager.ToString());
        //    return Ok(result);
        //}
        //else
        //{
        //    throw new IdentityOperationException(
        //        result.Errors.Select(e => e.Description)
        //    );
        //}
        throw new NotImplementedException();
    }


}
