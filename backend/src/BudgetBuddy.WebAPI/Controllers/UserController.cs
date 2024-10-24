using BudgetBuddy.Application.Services.Flow.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BudgetBuddy.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

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
