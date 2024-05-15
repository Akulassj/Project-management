using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using JIRA.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using JIRA.Shared.Entity;
using JIRA.Server.Domain.Repositories.Abstract;
using JIRA.Server.Domain.Repositories.EntityFramework;

namespace Authorization.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IUserRepository _userRepository;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null)
                return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!singInResult.Succeeded)
                return BadRequest("Invalid password");
            await _signInManager.SignInAsync(user, request.RememberMe);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest parameters)
        {
            try
            {
                var user = new User
                {
                    UserName = parameters.Login,
                    FirstName = parameters.FirstName,
                    LastName = parameters.LastName,
                    //TelegramChatId = parameters.TelegramChatId,
                };
                var result = await _userManager.CreateAsync(user, parameters.Password);
                if (!result.Succeeded)
                    return BadRequest(result.Errors.FirstOrDefault()?.Description);

                var roleExists = await _roleManager.RoleExistsAsync(parameters.Role);

                if (!roleExists)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(parameters.Role));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Error creating role.");
                    }
                }

                var role = await _userManager.AddToRoleAsync(user, parameters.Role);
                if (role.Succeeded)
                {
                    return await Login(new LoginRequest
                    {
                        Login = parameters.Login,
                        Password = parameters.Password
                    });
                }
                return await Login(new LoginRequest
                {
                    Login = parameters.Login,
                    Password = parameters.Password
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]   
        public async Task<CurrentUser> CurrentUserInfo()
        {
            var us = await _userManager.FindByNameAsync(User.Identity.Name);

                var roles = await _userManager.GetRolesAsync(us);
                var user = new CurrentUser
                {
                    Id = us.Id,
                    IsAuthenticated = User.Identity.IsAuthenticated,
                    UserName = User.Identity.Name,
                    Role = roles.Any() ? roles.First() :"",
                    Claims = User.Claims
                    .ToDictionary(c => c.Type, c => c.Value)
                };
                return user;

        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            
            return Ok(_userManager.Users.ToList());
        }

        [HttpGet]
        public IActionResult SearchUsers(string username)
        {
            var users = _userRepository.SearchUsersByUsername(username);
            return Ok(users);
        }
        
    }
}
