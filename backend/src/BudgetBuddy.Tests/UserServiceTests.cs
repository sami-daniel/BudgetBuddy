using AutoMapper;
using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;
using BudgetBuddy.Application.Services.Exceptions;
using BudgetBuddy.Application.Services.Flow.Abstractions;
using BudgetBuddy.Application.Services.Flow.Implementations;
using BudgetBuddy.Domain.Abstractions.Repository.Exceptions;
using BudgetBuddy.Domain.Abstractions.UnitOfWork;
using BudgetBuddy.Domain.Abstractions.Validator;
using BudgetBuddy.Domain.Entities;
using FakeItEasy;
using Xunit.Abstractions;

namespace BudgetBuddy.Tests;

public class UserServiceTests
{
    private readonly ITestOutputHelper _outputWriter;

    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly IMapper _fakeMapper;
    private readonly IValidatable<User> _fakeValidatableUser;
    private readonly IUserService _userService;

    public UserServiceTests(ITestOutputHelper outputHelper)
    {
        _outputWriter = outputHelper;
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeMapper = A.Fake<IMapper>();
        _fakeValidatableUser = A.Fake<IValidatable<User>>();
        _userService = new UserService(_fakeUnitOfWork, _fakeMapper, _fakeValidatableUser);
    }

    [Fact]
    public async Task RegisterUserAsync_WhenCalledWithValidUser_ShouldReturnUserResponse()
    {
        // Arrange
        var userAddRequest = new UserAddRequest
        {
            Username = "JhonDoe",
            UserPassword = "Password123!"
        };
        A.CallTo(() => _fakeValidatableUser.ValidateAsync(A<User>._))
            .Returns(new ValidationState(true, new Dictionary<string, string[]>()));
        A.CallTo(() => _fakeMapper.Map<User>(A<UserAddRequest>._))
            .Returns(new User
            {
                UserId = Guid.NewGuid(),
                Username = userAddRequest.Username,
                UserPassword = userAddRequest.UserPassword,
                CreatedAt = DateTime.UtcNow
            });
        A.CallTo(() => _fakeMapper.Map<UserResponse>(A<User>._))
            .Returns(new UserResponse
            {
                UserId = Guid.NewGuid(),
                Username = userAddRequest.Username,
                CreatedAt = DateTime.UtcNow
            });
        A.CallTo(() => _fakeUnitOfWork.UserRepository.AddAsync(A<User>._))
            .Returns(new User
            {
                UserId = Guid.NewGuid(),
                Username = userAddRequest.Username,
                UserPassword = userAddRequest.UserPassword,
                CreatedAt = DateTime.UtcNow
            });

        // Act
        var userResponse = await _userService.RegisterUserAsync(userAddRequest);
        _outputWriter.WriteLine(userResponse.ToString());

        // Assert
        Assert.NotNull(userResponse);
        Assert.Equal(userAddRequest.Username, userResponse.Username);
    }

    [Fact]
    public async Task RegisterUserAsync_WhenCalledWithInvalidUser_ShouldThrowValidationException()
    {
        // Arrange
        var userAddRequest = new UserAddRequest
        {
            Username = "JhonDoe",
            UserPassword = "Password123!"
        };
        A.CallTo(() => _fakeValidatableUser.ValidateAsync(A<User>._))
            .Returns(new ValidationState(false, new Dictionary<string, string[]> { { "Username", new[] { "Username is already taken" } } }));

        // Assert
        await Assert.ThrowsAsync<ValidationException>(async () =>
        {
            // Act
            await _userService.RegisterUserAsync(userAddRequest);
        });
    }

    [Fact]
    public async Task GetUserAsync_WhenCalledWithValidUserId_ShouldReturnUserResponse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByIdentifiersAsync(A<Guid>._))
            .Returns(new User
            {
                UserId = userId,
                Username = "JhonDoe",
                UserPassword = "Password123!",
                CreatedAt = DateTime.UtcNow
            });
        A.CallTo(() => _fakeMapper.Map<UserResponse>(A<User>._))
            .Returns(new UserResponse
            {
                UserId = userId,
                Username = "JhonDoe",
                CreatedAt = DateTime.UtcNow
            });

        // Act
        var userResponse = await _userService.GetUserByIDAsync(userId);
        _outputWriter.WriteLine(userResponse!.ToString());

        // Assert
        Assert.NotNull(userResponse);
        Assert.Equal(userId, userResponse.UserId);
    }

    [Fact]
    public async Task GetUserByIDAsync_WhenCalledWithInvalidUserId_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        A.CallTo(() => _fakeUnitOfWork.UserRepository.GetByIdentifiersAsync(A<Guid>._))
            .ThrowsAsync(new EntityNotFoundException("User not found"));

        // Act
        var userResponseResult = await _userService.GetUserByIDAsync(userId);

        // Assert
        Assert.Null(userResponseResult);
    }

    [Fact]
    public async Task GetUserByIDAsync_WhenCalledWithNullUserID_ShouldThrowArgumentNotFoundException()
    {
        // Arrange
        var userId = Guid.Empty;

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            // Act
            await _userService.GetUserByIDAsync(userId);
        });
    }

    [Fact]
    public async Task RegisterUserAsync_WhenCalledWithNullUser_ShouldThrowArgumentNotFoundException()
    {
        // Arrange
        UserAddRequest? userAddRequest = null;

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            // Act
            await _userService.RegisterUserAsync(userAddRequest!);
        });
    }
}
