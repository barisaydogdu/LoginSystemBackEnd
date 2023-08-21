using LoginSystemBackEnd.Models;

namespace LoginSystemBackEnd.Services
{
    public interface IUserService
    {
        Task<Users>Authenticate(UserLoginModel  userCredentials);
        Task<Users> Register(UserCredentials userCredentials);
    }
}
