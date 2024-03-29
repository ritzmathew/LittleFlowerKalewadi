using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LittleFlowerKalewadi.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using LittleFlowerKalewadi.Server.Data;
using LittleFlowerKalewadi.Server;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace LittleFlowerKalewadi.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly ApplicationDbContext _context;
        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this._context = context;
        }
        [AllowAnonymous]
        [HttpPut("registeruser")]
        public async Task<User> RegisterUser([FromBody] User user)
        {
            User newUser = new User();
            User userExists = await _context.Users
                                        .Where(u => u.EmailAddress == user.EmailAddress)
                                        .FirstOrDefaultAsync();
            if(userExists == null) {
                newUser.UserId = _context.Users.Any() ? _context.Users.Max(user => user.UserId) + 1 : 1;
                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.DateOfBirth = user.DateOfBirth;
                newUser.CreatedDate = DateTime.Now;
                newUser.EmailAddress = user.EmailAddress;
                newUser.Password = Utility.Encrypt(user.Password); 
                newUser.RoleId = 99;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }
            return await Task.FromResult(newUser);
        }
        [HttpPost("updateprofile")]
        public async Task<User> UpdateProfile([FromBody] User user)
        {
            User _user = await _context.Users
                                        .Where(u => u.UserId == user.UserId)
                                        .FirstOrDefaultAsync();
            if(_user != null) {
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.DateOfBirth = user.DateOfBirth;
                _user.CreatedDate = user.CreatedDate;
                _user.EmailAddress = user.EmailAddress;
                _context.Users.Update(_user);
                await _context.SaveChangesAsync();
            }
            return await Task.FromResult(_user);
        }
        [AllowAnonymous]
        [HttpPost("loginuser")]
        public async Task<ActionResult<string>> LoginUser(User user)
        {
            user.Password = Utility.Encrypt(user.Password);
            User loggedInUser = await _context.Users
                                        .Include(u => u.Role)
                                        .Where(u => u.EmailAddress == user.EmailAddress && u.Password == user.Password)
                                        .FirstOrDefaultAsync();
            if (loggedInUser != null)
            {
                //create a claim
                var claimEmailAddress = new Claim(ClaimTypes.Email, loggedInUser.EmailAddress);
                var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, Convert.ToString(loggedInUser.UserId));
                var claimRole = new Claim(ClaimTypes.Role, loggedInUser.Role.RoleDesc);
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claimEmailAddress, claimNameIdentifier, claimRole }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Sign In User
                await HttpContext.SignInAsync(claimsPrincipal);
            } else {
                return Unauthorized();
            }
            return await Task.FromResult(loggedInUser.EmailAddress);
        }
        [HttpPost("resetpassword")]
        public async Task<string> ResetPassword(User user)
        {
            if (User.Identity.IsAuthenticated)
            {
                user.Password = Utility.Encrypt(user.Password);
                user.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User _user = await _context.Users
                                        .Where(u => u.UserId == user.UserId)
                                        .FirstOrDefaultAsync();
                if (_user != null)
                {
                    _user.Password = user.Password;
                    _context.Users.Update(_user);
                    await _context.SaveChangesAsync();
                }
            }               

            return await Task.FromResult("Success");
        }
        [AllowAnonymous]
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                currentUser = await _context.Users
                                            .Include(u => u.Role)
                                            .Where(u => u.UserId == userId)
                                            .FirstOrDefaultAsync();
                currentUser.Role.Users = null;

            }
            return await Task.FromResult(currentUser);
        }
        [HttpGet("logoutuser")]
        public async Task<ActionResult<string>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        }
        
        [HttpGet("getprofile/{userId}")]
        public async Task<User> GetProfile(int userId)
        {
            return await _context.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
        }
        [HttpGet("getunassigned")]
        public async Task<ActionResult<IEnumerable<User>>> GetPublishers()
        {
            return await _context.Users.Where(u => u.RoleId == 99).ToListAsync();
        }
    }
}