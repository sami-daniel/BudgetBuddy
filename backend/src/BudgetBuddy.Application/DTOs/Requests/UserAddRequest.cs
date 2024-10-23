namespace BudgetBuddy.Application.DTOs.Requests;

public class UserAddRequest
{
    public string Username { get; set; } = null!;
    public string UserPassword { get; set; } = null!;
}
