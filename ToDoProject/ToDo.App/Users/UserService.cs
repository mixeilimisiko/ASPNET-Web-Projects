using Mapster;
using ToDo.App.Mappings;
using ToDo.App.Users.Requests;
using ToDo.App.Users.Responses;
using ToDo.Domain;
using ToDo.App.Exceptions;

namespace ToDo.App.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseModel> LogIn(UserLoginRequestModel userRequest, CancellationToken token)
        {
            var user = await _repository.ReadByUsernameAsync(userRequest.Username, token);
            if (user == null)
            {
                throw new NotFoundError("User not found");
            }

            if (!PasswordHasher.VerifyPassword(userRequest.Password, user.PasswordHash))
            {
                throw new InvalidPasswordError("Invalid password.");
            }

            var userResponse = user.Adapt<UserResponseModel>();

            return userResponse;
        }

        public async Task<int> Register(UserRegisterRequestModel userRequest, CancellationToken token)
        {

            var existingUser = await _repository.ReadByUsernameAsync(userRequest.Username, token);
            if (existingUser != null)
            {
                throw new AlreadyExistsError("User already exists");
            }

            var user = userRequest.Adapt<User>();
            await _repository.CreateAsync(user, token);
            return user.Id;

        }
    }
}
