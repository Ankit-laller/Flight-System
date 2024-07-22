using Flight_System.Interfaces;
using Flight_System.MOdels;
using Flight_System.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Flight_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo  _userRepo;
        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/<UserController>
        [HttpPost("userLogin")]
        public async Task<ActionResult> Login(UserLoginDto value)
        {
            var data = await _userRepo.userLogin(value);
            return Ok(data);
        }
        [HttpPost("userRegister")]
        public async Task<ActionResult> Register(UserRegistrationDto value)
        {
            var data = await _userRepo.userRegistration(value);
            return Ok(data);
        }

        [HttpPost("createSubUser")]
        public async Task<ActionResult> RegisterSub(SubUserRegistrationDto value)
        {
            var data = await _userRepo.createSubUser(value);
            return Ok(data);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var data = await _userRepo.getUserById(id);
            return Ok(data);
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
