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

namespace BlazingChat.Server.Controllers
{
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
        
        //Authentication Methods
        [HttpPost("loginuser")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            User loggedInUser = await _context.Users
                                        .Where(u => u.EmailAddress == user.EmailAddress && u.Password == user.Password)
                                        .FirstOrDefaultAsync();

            if (loggedInUser != null)
            {
                //create a claim
                var claim = new Claim(ClaimTypes.Name, loggedInUser.EmailAddress);
                //create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");
                //create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //Sign In User
                var done = HttpContext.SignInAsync(claimsPrincipal);
                HttpContext.User = claimsPrincipal;
                await Task.FromResult(done);
            }

            return await Task.FromResult(loggedInUser);
        }
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();
            if (User.Identity.IsAuthenticated)
            {
                currentUser.EmailAddress = User.FindFirstValue(ClaimTypes.Name);
            }
            return await Task.FromResult(currentUser);
        }
        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        }  
    }
}