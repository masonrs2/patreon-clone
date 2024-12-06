using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using Paramatic.Models;
using Paramatic.Services;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System;
using Amazon.DynamoDBv2;
using System.Text;  // For Encoding
using System.Security.Claims;  // For ClaimsIdentity and Claim
using Microsoft.IdentityModel.Tokens;  // For SecurityTokenDescriptor, SigningCredentials
using System.IdentityModel.Tokens.Jwt;  // For JwtSecurityTokenHandler

namespace Paramatic.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user) 
        {
            var createdUser = await _userService.CreateUserAsync(user);
            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, User user) 
        {
            var updatedUser = await _userService.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _userService.DeleteUserByIdAsync(id);
            return NoContent();
        }
    }
}