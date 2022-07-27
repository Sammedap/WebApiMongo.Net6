using Microsoft.AspNetCore.Mvc;
using UserManagerService.UserManagerService;
using UserModel.UserModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService<User> userService;

        public UsersController(IUserService<User> userService)
        {
            this.userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            //throw new System.Exception("An error occurred");
            return userService.Get().ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            //Sample error generation
            //if (!string.Equals(id?.TrimEnd(), "Redmond", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new ArgumentException(
            //        $"We don't offer a weather forecast for {id}.", nameof(id));
            //}

            var user = userService.Get(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found");
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            //CreatedAtAction returns httpstatus code 201 and also sets location header using which we can access newly
            //created user object it needs id which we pass in second parameter, 3rd parameter is user object itesef
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] User user)
        {
            var existingUser = userService.Get(id);
            if (existingUser == null)
            {
                return NotFound($"Student with Id = {id} not found");
            }
            userService.Update(id, user);

            return Ok(existingUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {

            var user = userService.Get(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }
            userService.Remove(id);
            return Ok($"Student with Id = {id} deleted.");
        }
    }
}
