using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.API.Identity.Jwt;
using OnlineStore.API.Identity.Models;
using OnlineStore.API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public AccountController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await userManager.AddToRoleAsync(user, "User");

            return Ok();
        }

        [HttpPost]
        [Route("token")]
        [Consumes("application/json")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null) return BadRequest("Пользователь не найден");

            var passwordIsValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordIsValid) return BadRequest("Неверный пароль");

            var claims = await userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, (await userManager.GetRolesAsync(user))[0]));

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(AuthOptions.LIFETIME_DAYS)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = model.Email
            };

            return Ok(response);
        }


        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            var roles = await userManager.GetRolesAsync(user);
            var inRoleUser = await userManager.IsInRoleAsync(user, "User");

            return Ok(new
            {
                UserName = userName,
                Roles = roles,
                IsInRoleUser = inRoleUser
            });
        }
    }
}
