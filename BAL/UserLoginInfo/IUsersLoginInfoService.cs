using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.UserLoginInfo
{
    public interface IUsersLoginInfoService
    {
        Task AddUserRole(User user);
        Task<bool> CheckUserEmail(string email);
        Task<User> AddNewUser(User user);
        Task AccessFailed(User user);
        Task ResetAccessFailedCount(User user);
        //Task ResetLockoutEndDateAsync(User user);
        Task<bool> CheckUserPassword(User user, string password);
        Task<bool> UserIsLockedOut(User user);
        Task<User> GetUser(int userId);
        Task<User> GetUser(string userName);
        Task<bool> DoUserExists(string userName);
    }
}
