using AutoMapper;
using BudgetBuddy.Application.DTOs.Requests;
using BudgetBuddy.Application.DTOs.Responses;
using BudgetBuddy.Domain.Entities;

namespace BudgetBuddy.Application.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAddRequest, User>().ForMember(user => user.UserId, options => options.Ignore())
                                         .ForMember(user => user.CreatedAt, options => options.Ignore());

        CreateMap<User, UserResponse>().ForSourceMember(user => user.UserPassword, options => options.DoNotValidate());
    }
}
