using ToDo.App.Users.Requests;
using ToDo.App.Users.Responses;

namespace ToDo.App.Users
{
    public interface IUserService
    {
        Task<UserResponseModel> LogIn(UserLoginRequestModel userRequest, CancellationToken token);
        Task<int> Register(UserRegisterRequestModel userRequest, CancellationToken token);
    }
}
