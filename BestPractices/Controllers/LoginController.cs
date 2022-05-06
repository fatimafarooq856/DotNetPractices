using App.Common.Abstract.Helpers;
using App.Common.Campatibility.Filters;
using App.Utils.Entities;
using App.Utils.Helpers;
using BAL.UserLoginInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NodaTime;

namespace BestPractices.Controllers
{
    [Route("api/v1/token")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersLoginInfoService _usersLoginInfoService;
        private readonly IOptions<AppApiOptions> _options;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IUsersService _usersService;
        //private readonly IClientService _clientService;
        //private readonly IRoleService _roleService;
        //private readonly IUserSharedService _userSharedService;

        public LoginController(
            IUsersLoginInfoService usersLoginInfoService,
            IOptions<AppApiOptions> options,
            IWebHostEnvironment webHostEnvironment
        //IUsersService usersService,
        //IClientService clientService,
        //IRoleService roleService,
        //IUserSharedService userSharedService
        )
        {
            _usersLoginInfoService = usersLoginInfoService;
            _options = options;
            _webHostEnvironment = webHostEnvironment;
            //_usersService = usersService;
            //_clientService = clientService;
            //_roleService = roleService;
            //_userSharedService = userSharedService;
        }

        const string InvalidUsernameOrPassword = "Invalid username or password";

        [HttpPost("loginfirst")]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<IActionResult> GetLoginToken([FromBody] LoginModel model)
        {
            if (!(await _usersLoginInfoService.DoUserExists(model.UserName)))
                throw new FrontendInformationException(InvalidUsernameOrPassword);

            var user = await _usersLoginInfoService.GetUser(model.UserName);

            if (await _usersLoginInfoService.UserIsLockedOut(user))
                throw new FrontendInformationException("User is locked out.");

            if (await _usersLoginInfoService.CheckUserPassword(user, model.Password))
            {
                await _usersLoginInfoService.ResetAccessFailedCount(user);
            }
            else
            {
                await _usersLoginInfoService.AccessFailed(user);

                if (await _usersLoginInfoService.UserIsLockedOut(user)) throw new FrontendInformationException("To many login attempts, user is locked out.");

                throw new FrontendInformationException(InvalidUsernameOrPassword);
            }

            var expires = SystemClock.Instance.GetCurrentInstant().Plus(Duration.FromMinutes(10));
            //var userRoles = new[] { TokenRoles.LoginToken };
            //userRoles,
            var token = TokenHelper.GetToken(user.Id, _options.Value.Token, expires);
            AddSignatureToContextAndModifyToken(HttpContext, token);

            return Ok(token);
        }
        private void AddSignatureToContextAndModifyToken(HttpContext context, TokenHelper.TokenResponse token)
        {
            if (_webHostEnvironment.IsProduction() && _options.Value.UseHttps)
            {
                var correctSignature = TokenHelper.ModifyTokenAndReturnSignature(token);
                context.Response.Cookies.Append("signature", correctSignature, new CookieOptions()
                {
                    SameSite = SameSiteMode.Strict,
                    Expires = token.Expires.ToDateTimeOffset(),
                    HttpOnly = true,
                    Secure = true
                });
            }
        }
    }
}
