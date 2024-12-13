using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuitQBackend.Models;
using QuitQBackend.Repositories;

namespace QuitQBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {

        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllUser() {
            List<User> user=_service.GetAllUsers();

            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) {
            User user = _service.GetUserById(id);

            if (user == null) {
                return NotFound();
            }
            return Ok(user);
        }

        //[HttpPost]
        //public IActionResult Post(User user) {
        //    int result=_service.AddNewUser(user);


        //    return Ok(result);
        //}


        [HttpPut("{id}")]
        public IActionResult Update(int id,User user) {
            if (id!=user.Id) return BadRequest();
            User result = _service.UpdateUser(user);
            return Ok(result);
        }

        
        [HttpDelete]
        public IActionResult Delete(int id) {
            string result=_service.DeleteUser(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
