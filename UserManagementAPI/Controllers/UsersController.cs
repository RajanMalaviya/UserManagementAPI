using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found." });
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || !IsValidEmail(user.Email))
            {
                return BadRequest(new { error = "Invalid user data." });
            }

            user.Id = users.Max(u => u.Id) + 1; // Simple ID generation
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            if (string.IsNullOrEmpty(updatedUser.Name) || !IsValidEmail(updatedUser.Email))
            {
                return BadRequest(new { error = "Invalid user data." });
            }

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found." });

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return NoContent(); // Success but no content to return
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found." });

            users.Remove(user);
            return NoContent(); // Success but no content to return
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }
}
