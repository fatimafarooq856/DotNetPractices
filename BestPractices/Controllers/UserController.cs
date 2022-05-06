using App.Common.Campatibility.Extensions;
using BAL.UserService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BestPractices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserInterface _user;
        public UserController(IUserInterface user)
        {
            _user = user;

        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _user.GetUsers();
            return Ok(result);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(UserDto userObj)
        {
            var userId = User.GetOriginalOrDefaultUserId();
            var getUserId = await _user.AddUsers(userObj, userId);
            return Ok(getUserId);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UserDto userObj)
        {
            var userId = User.GetOriginalOrDefaultUserId();
            await _user.UpdateUsers(id, userObj, userId);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
