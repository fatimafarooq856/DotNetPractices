using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.UserLoginInfo
{
	public class UsersLoginInfoService : IUsersLoginInfoService
	{
		private readonly UserManager<User> _userManager;

		public UsersLoginInfoService(
			UserManager<User> userManager
		)
		{
			_userManager = userManager;
		}
		public async Task<User> AddNewUser(User user)
		{
			var result = await _userManager.CreateAsync(user);
			if (!result.Succeeded)
				throw GetIdentityException(result);

			return user;
		}
		public async Task AddUserRole(User user)
		{
			await _userManager.AddToRoleAsync(user, "ADMIN");
		}
		
		public async Task<bool> CheckUserEmail(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			return (user != null)? true: false ;
			
		}
		public async Task<bool> CheckUserPassword(User user, string password)
		{
			return await _userManager.CheckPasswordAsync(user, password);
		}
		public async Task ResetAccessFailedCount(User user)
		{
			var result = await _userManager.ResetAccessFailedCountAsync(user);

			if (!result.Succeeded)
				throw GetIdentityException(result);
		}
		
		//private async Task ResetLockoutEndDateAsync(User user)
		//{
		//	var result = await _userManager.SetLockoutEndDateAsync(user, null);

		//	if (!result.Succeeded)
		//		throw GetIdentityException(result);
		//}
		public async Task<bool> UserIsLockedOut(User user)
		{
			return await _userManager.IsLockedOutAsync(user);
		}

		public async Task AccessFailed(User user)
		{
			var result = await _userManager.AccessFailedAsync(user);

			if (!result.Succeeded)
				throw GetIdentityException(result);
		}
		public async Task<User> GetUser(int userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());

			if (user == null)
				throw new ApplicationException($"User ({userId}) not found!");

			return user;
		}

		public async Task<User> GetUser(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName.ToLower());

			if (user == null)
				throw new ApplicationException($"User {userName} not found!");

			return user;
		}

		public async Task<bool> DoUserExists(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName.Trim());
			if (user == null)
				return false;

			return true;
		}

		private Exception GetIdentityException(IdentityResult identityResult)
		{
			if (!identityResult.Succeeded)
			{
				var errors = identityResult.Errors.Select(x => x.Description);
				var formattedErrors = string.Join(Environment.NewLine, errors);
				return new ApplicationException(formattedErrors);
			}
			else
			{
				return new ApplicationException("Idenity sucesseded, not an TranslationException");
			}
		}
	}
}
