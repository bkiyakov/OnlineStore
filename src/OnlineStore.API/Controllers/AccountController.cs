using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.Identity.Models;
using OnlineStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
