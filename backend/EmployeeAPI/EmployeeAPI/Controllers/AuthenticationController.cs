using EmployeeAPI.Services;
using EmployeeManagement.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticate a user by providing login credentials.
        /// </summary>
        /// <remarks>
        /// This endpoint allows users to authenticate by providing their login credentials.
        /// If the provided credentials are valid, the user is authenticated and a token is generated.
        /// </remarks>
        /// <param name="model">The login credentials of the user.</param>
        /// <returns>Returns an IActionResult indicating the result of the login attempt.</returns>
        /// <response code="200">Returns an OK response with a success message if authentication is successful.</response>
        /// <response code="400">Returns a Bad Request response with an error message if the provided payload is invalid or authentication fails.</response>
        /// <response code="500">Returns a Server Error response if an unexpected error occurs during authentication.</response>
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message, user) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);

                var cookieOptions = new CookieOptions()
                {
                    IsEssential = true,
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None
                };

                Response.Cookies.Append("X-Access-Token", message, cookieOptions);

                return Ok(new { token = message, userDetails = user });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Register a new user account.
        /// </summary>
        /// <remarks>
        /// This endpoint allows users to register a new account by providing their registration details.
        /// If the registration is successful, the user account is created and a success response is returned.
        /// </remarks>
        /// <param name="model">The registration details of the user.</param>
        /// <returns>Returns an IActionResult indicating the result of the registration attempt.</returns>
        /// <response code="201">Returns a Created response with the newly created user account details if registration is successful.</response>
        /// <response code="400">Returns a Bad Request response with an error message if the provided payload is invalid or registration fails.</response>
        /// <response code="500">Returns a Server Error response if an unexpected error occurs during registration.</response>
        [Route("registration")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
