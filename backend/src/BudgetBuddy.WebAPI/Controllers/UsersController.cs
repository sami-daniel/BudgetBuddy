using BudgetBuddy.Application.Services.Flow.Abstractions;
using BudgetBuddy.Application.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    /// <summary>
    /// Gets the user corresponding to the provided name.
    /// </summary>
    /// <param name="userID">Name of the user to be retrieved.</param>
    /// <returns>A <see cref="UserResponse"/> object representing the found user.</returns>
    /// <response code="200">User successfully found.</response>
    /// <response code="404">User with the userID not found.</response>
    [HttpGet("{userID:guid}")]
    public async Task<IActionResult> GetUserByID(Guid userID)
    {
        var user = await _userService.GetUserByIDAsync(userID);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
