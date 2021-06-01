using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peer_Review_11.Models;
using Peer_Review_11.Services;

namespace Peer_Review_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Get users
        /// </summary>
        /// <returns>All available users.</returns>
        [HttpGet("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            if (UsersService.GetAll().Count == 0)
                return NotFound("No users have been added yet.");
            return Ok(UsersService.GetAll());
        }

        /// <summary>
        /// Get the user with a specified email.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user with the specified email</returns>
        /// <response code="400">User does not exist.</response>
        [HttpGet("Get user by {id}")]
        public IActionResult GetUserById(string id)
        {
            var user = UsersService.GetByEmail(id);
            if (user == null)
                return NotFound("User with this email does not exist.");
            return Ok(user);
        }

        /// <summary>
        /// Initializes 10 users randomly if there are no users defined already.
        /// </summary>
        /// <returns></returns>
        [HttpPost("InitializeUsers")]
        public IActionResult InitializeUsers()
        {
            if (UsersService.GetAll().Count != 0)
                return BadRequest("There are existing users already.");
            
            UsersService.RandomInitialize();
            return Ok(UsersService.GetAll());

        }
        
        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Validity of input user</returns>
        [HttpPost("CreateUser")]
        [ProducesResponseType(200)]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                UsersService.Add(user);
                return Ok("User has been successfully added.");
            }

            return BadRequest("User is not valid");
        }

        /// <summary>
        /// Gets all the users starting from the {offset} and continuing {limit} positions ahead.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("GetUsersWithLimits")]
        public IActionResult GetUsersWithLimits(int offset, int limit)
        {
            if (offset > UsersService.GetAll().Count || offset < 1)
                return BadRequest("Offset value is not valid.");
            if (limit < 1)
                return BadRequest("Limit cannot be less than 1.");

            var result = UsersService.GetAllWithLimits(offset, limit);
            if (result.Count == 0)
                return NotFound(
                    "No users with the specified criteria were found.");
            return Ok(result);
        }
        
    }
}