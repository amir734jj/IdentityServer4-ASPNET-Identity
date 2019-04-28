using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Attributes;
using Api.Configs;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IEventService _events;

        public AccountController(IOptions<JwtSettings> jwtSettings, UserManager<User> userManager,
            SignInManager<User> signManager, IEventService events)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signManager = signManager;
            _events = events;
        }

        [SecurityHeaders]
        [HttpGet]
        [Route("Test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Authenticated!");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return User.Identity.IsAuthenticated
                ? Ok(await _userManager.GetUserAsync(HttpContext.User))
                : Ok(new object());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Register")]
        public async Task<IActionResult> RegisterIndex()
        {
            return Ok("Please log-in by posting to this route!");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterHandler([FromBody] RegisterViewModel registerViewModel)
        {
            var user = new User
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                Fullname = registerViewModel.Fullname
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            
            // ReSharper disable once InvertIf
            if (result.Succeeded)
            {
                await _signManager.SignInAsync(user, true);
                return Ok("Successfully registerd!");
            }

            return BadRequest("Failed to register!");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> LoginIndex()
        {
            return Ok("Please log-in by posting to this route!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginHandler([FromBody] LoginViewModel loginViewModel)
        {
            var result =
                await _signManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, true, false);

            if (result.Succeeded)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Key));

                await _events.RaiseAsync(new UserLoginSuccessEvent(loginViewModel.Username, User.GetSubjectId(), User.GetDisplayName()));

                var token = new JwtSecurityToken(
                    _jwtSettings.Value.Issuer,
                    _jwtSettings.Value.Audience,
                    User.Claims.Concat(new []{ new Claim(ClaimTypes.Name, loginViewModel.Username) }),
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddMinutes(_jwtSettings.Value.AccessTokenDurationInMinutes),
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return BadRequest("Failed to log-in!");
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> LoginOut()
        {
            await _signManager.SignOutAsync();
            
            // raise the logout event
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

            return Ok("Logged-Out");
        }

        [HttpGet]
        [Route("Forbidden")]
        public async Task<IActionResult> Forbidden()
        {
            return Ok("Forbidden");
        }
    }
}