using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Management.Models;
using System.Security.Cryptography;

namespace Management.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly TodoContext _context;

        public UserProfileController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems 
        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetAllUsers()
        {
            List<UserProfile> userList = await _context.UserProfile.ToListAsync();
            var map = new Dictionary<string, List<UserProfile>>();
            map["users"] = userList;
            var response = new ApiResponse<Dictionary<String, List<UserProfile>>>
            {
                Status = true,
                Message = "success",
                Data = map
            };
            return Ok(response);
        }

        // GET: api/TodoItems/5
        [HttpGet("getUserById")]
        public async Task<ActionResult<UserProfile>> GetUserById(String uid)
        {
            var userList = await _context.UserProfile.ToListAsync();
            var userProfile = userList.FirstOrDefault(user=> user.Uid == uid);

            if (userProfile == null)
            {
                var response = new ApiResponse<UserProfile>
                {
                    Status = false,
                    Message = "User not found",
                    Data = userProfile
                };
                return NotFound(response);
            } else
            {
                var response = new ApiResponse<UserProfile>
                {
                    Status = true,
                    Message = "User retrieved successfully",
                    Data = userProfile
                };
                return Ok(response);
            }
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(String Uid, UserProfile userProfile)
        {
            if (Uid != userProfile.Uid)
            {
                return BadRequest();
            }

            _context.Entry(userProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(Uid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile(UserProfile userProfile)
        {
            var utils = new Utils();
            userProfile.Uid = Utils.GenerateRandomText(15);
            _context.UserProfile.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllUsers), new { Uid = userProfile.Uid }, userProfile);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(String Uid)
        {
            var todoItem = await _context.UserProfile.FindAsync(Uid);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.UserProfile.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProfileExists(String Uid)
        {
            return _context.UserProfile.Any(e => e.Uid == Uid);
        }
    }
}
