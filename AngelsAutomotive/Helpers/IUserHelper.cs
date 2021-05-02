using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AngelsAutomotive.Helpers
{
    public interface IUserHelper
    {
        //Everything that has to do with user management is done here (bypass of UserManager)

        Task<IdentityResult> AddUserAsync(User user, string password);


        Task<User> GetUserByEmailAsync(string email);


        Task<SignInResult> LoginAsync(LoginViewModel model);


        Task LogOutAsync();


        Task<IdentityResult> UpdateUserAsync(User user);//update user data


                Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);//as the password data is more sensitive, we do a separate method


        Task CheckRoleAsync(string roleName);


        Task<bool> IsUserInRoleAsync(User user, string roleName);


        Task AddUserToRoleAsync(User user, string roleName);


        Task<string> GenerateEmailConfirmationTokenAsync(User user);


        Task<IdentityResult> ConfirmEmailAsync(User user, string token);


        Task<User> GetUserByIdAsync(string userId);


        Task<string> GeneratePasswordResetTokenAsync(User user);


        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);

    }
}
