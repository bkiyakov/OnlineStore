using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.API.Identity;
using OnlineStore.API.Identity.Jwt;
using OnlineStore.API.Identity.Models;
using OnlineStore.API.Models;
using OnlineStore.Application.Dtos;
using OnlineStore.Application.Services.Interfaces;
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
    [Authorize(Roles = Roles.Manager)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICustomerService customerService;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            ICustomerService customerService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.customerService = customerService;
        }

        [HttpPost]
        [Route("register")]
        [Consumes("application/json")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var roles = await roleManager.Roles
                .Select(identityRole => identityRole.Name)
                .ToListAsync();

            if (!roles.Contains(model.Role))
            {
                ModelState.AddModelError("Role", "Некорректная роль");

                return BadRequest(ModelState);
            }

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

            await userManager.AddToRoleAsync(user, model.Role);

            var userId = await userManager.GetUserIdAsync(user);
            var customerDto = new CustomerDto
            {
                UserId = userId,
                Name = model.Name,
                Code = model.Code,
                Address = model.Address,
                Discount = model.Discount
            };

            bool isCustomerAdded = false;

            try
            {
                var addedCustomer = await customerService.AddCustomerAsync(customerDto);

                if(addedCustomer != null)
                {
                    isCustomerAdded = true;
                } else
                {
                    return BadRequest("Регистрация не удалась");
                }
            }
            finally
            {

                if (!isCustomerAdded)
                {
                    await userManager.DeleteAsync(user);
                }
            }
            

            return Ok();
        }

        [HttpGet]
        [Route("get-rolenames")]
        public async Task<IActionResult> GetRoleNames()
        {
            var roles = await roleManager.Roles
                .Select(identityRole => identityRole.Name)
                .ToListAsync();

            return Ok(roles);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userManager.Users.ToListAsync();
            var userModels = new List<UserModel>();

            if(users != null)
            {
                foreach (var user in users)
                {
                    var userRole = (await userManager.GetRolesAsync(user))[0];

                    userModels.Add(new UserModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = userRole
                    });
                }
            }

            return Ok(userModels);
        }

        [HttpGet]
        [Route("get/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        public async Task<IActionResult> GetManager(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return BadRequest("Пользователь не найден");

            var userRole = (await userManager.GetRolesAsync(user))[0];

            var userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = userRole
            };

            return Ok(userModel);
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public async Task<IActionResult> DeleteManager(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return BadRequest("Пользователь не найден");

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("edit/{userId}")]
        [Consumes("application/json")]
        public async Task<IActionResult> EditManager([FromBody] EditUserInputModel model, string userId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return BadRequest("Пользователь не найден");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
        }

        [AllowAnonymous]
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
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
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

        [Authorize]
        [HttpPost]
        [Route("change-password")]
        [Consumes("application/json")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInputModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userManager.GetUserAsync(User);

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            } else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
        }

        //[Authorize(Roles = Roles.User)]
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
