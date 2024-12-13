using CaseStudyQuitQ.Exceptions;
using CaseStudyQuitQ.Models;
using CaseStudyQuitQ.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase { // Provides base functionality for API controllers that don’t serve views.
        private IUserService _service;
        public UsersController(IUserService service) {
            _service = service;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUser() {
            try {
                List<User> user = await _service.GetAllUsers();
                if (user == null) throw new NullObjectException("No user was found");
                return Ok(user);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Seller")]
        public async Task<IActionResult> GetAllSeller() {
            try {
                List<User> user = await _service.GetAllSeller();
                if (user == null) throw new NullObjectException("No user was found");
                return Ok(user);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Customer")]
        public async Task<IActionResult> GetAllCustomer() {
            try {
                List<User> user = await _service.GetAllCustomer();
                if (user == null) throw new NullObjectException("No user was found");
                return Ok(user);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                User user = await _service.GetUserById(id);
                if (user == null) throw new NullObjectException("No user was found with id: "+ id);
                return Ok(user);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }


        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserByUserName(string userName) {
            try {
                if (userName == null) throw new InvalidIdException("userName is REquired");
                User user = await _service.GetUserByUsername(userName);
                if (user == null) throw new NullObjectException("No user was found with id: " + userName);
                return Ok(user);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }


        }


        [Authorize(Roles = "Customer")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, User user) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                if (id != user.Id) throw new InvalidIdException("Id should be same as userId");
                User result = await _service.UpdateUser(user);
                if (result == null) throw new NullObjectException("No user was found to update");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }

        }


        [Authorize(Roles = "Admin,Customer")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            try {
                if (id == 0) throw new InvalidIdException("Id cannot be 0");
                string result = await _service.DeleteUser(id);
                if (result == null) throw new NullObjectException("No user was found to delete");
                return Ok(result);
            }
            catch (InvalidIdException e) {
                return BadRequest(e.Message);
            }
            catch (NullObjectException e) {
                return NotFound(e.Message);
            }
        }
    }
} 
