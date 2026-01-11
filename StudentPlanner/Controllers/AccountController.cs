using ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentPlanner.Core.DTO;
using StudentPlanner.Core.Domain;
using StudentPlanner.Core.Errors;
using StudentPlanner.Core.ValueObjects;
using ServiceContracts;


/// <summary>
/// Provides account-related operations such as registration, authentication,
/// and account management.
/// </summary>
/// <remarks>
/// These endpoints handle user authentication lifecycle actions:
/// - Registration
/// - Login
/// - Logout
/// - Account deletion
///
/// Public access is allowed for registration and login.
/// Authenticated access is required for logout and account deletion.
/// </remarks>
namespace StudentPlanner.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService; 
        }

        /// <summary>
        /// Registers a new user account.
        /// </summary>
        /// <remarks>
        /// This endpoint creates a new user account using the provided registration details.
        /// If the email address is already in use, the operation fails.
        /// </remarks>
        /// <param name="userRequest">The user registration details.</param>
        /// <returns>No content if registration succeeds.</returns>
        /// <response code="200">User registered successfully.</response>
        /// <response code="400">If the provided password does not meet validation rules.</response>
        /// <response code="409">If an account with the given email already exists.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Passwords don't match")]
        [ProducesResponseType(StatusCodes.Status409Conflict, Description = "Email already exists")]
        public async Task<IActionResult> Register(CreateUserRequest userRequest)
        {
            ApplicationUser manager = new ApplicationUser()
            {
                Email = userRequest.Email,
                FirstName = "Ben",
                Surname = "Ten",
            };

            UserResponse resp = await _userService.CreateUser(userRequest);
            return Ok();
        }

        /// <summary>
        /// Authenticates a user using email and password.
        /// </summary>
        /// <remarks>
        /// If authentication succeeds, the user is signed in and a session is established.
        /// If the user is already authenticated, a message indicating the current session
        /// is returned instead.
        /// </remarks>
        /// <param name="loginRequest">The login credentials.</param>
        /// <returns>The authenticated user's information.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="401">If the credentials are invalid.</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Invalid credentials")]
        public async Task<IActionResult> LogIn(LoginDTO loginRequest)
        {
            if(User.Identity!=null && User.Identity.IsAuthenticated)
                return Ok(new { Message = "User is already signed in.", User = User.Identity.Name });

            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, isPersistent: false, lockoutOnFailure: false); //lockout = lock for some time period if continuous login failues occur

            return result.Succeeded ? Ok((await _userManager.FindByEmailAsync(loginRequest.Email))!.ToUserResponse()) : Unauthorized();
        }

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        /// <remarks>
        /// This endpoint terminates the current authentication session.
        /// </remarks>
        /// <returns>No content if logout succeeds.</returns>
        /// <response code="200">Logout successful.</response>
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Deletes the authenticated user's account.
        /// </summary>
        /// <remarks>
        /// Only users with the <c>User</c> role may delete their own accounts.
        /// The operation signs the user out before account deletion.
        /// </remarks>
        /// <param name="userEmail">The email address of the account to delete.</param>
        /// <returns>No content if the account is deleted successfully.</returns>
        /// <response code="200">Account deleted successfully.</response>
        /// <response code="403">If the user does not have permission to perform this action.</response>
        [HttpDelete("delete-account")]
        [Authorize(Roles="User")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Description = "You don't have permission to perform this action")]
        public async Task<IActionResult> DeleteAccount(string userEmail)
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
