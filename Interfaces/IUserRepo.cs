using Flight_System.MOdels;
using Flight_System.Utils;

namespace Flight_System.Interfaces
{
    public interface IUserRepo
    {
        Task<UserLoginResponseClass> userLogin(UserLoginDto value);
        Task<UserRegistrationResponse> userRegistration(UserRegistrationDto value);
        Task<UserRegistrationResponse> createSubUser(SubUserRegistrationDto value);
        Task<UserLoginResponseClass> getUserById(String id);
    }
}
