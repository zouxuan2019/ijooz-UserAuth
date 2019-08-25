using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserAuth.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _usermanager;
        private SignInManager<ApplicationUser> _signinmanager;
        private readonly ApplicationSettings _appSettings;


        public ApplicationUserController(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signinmanager, IOptions<ApplicationSettings> appSettings)
        {
            _usermanager = usermanager;
            _signinmanager = signinmanager;
            _appSettings = appSettings.Value;

        }

        [HttpPost]
        [Route("Register")]
        //POST: /api/ApplicationUser/Register


        public async Task<Object> PostApplicationUser(ApplicationUserModel model)
        {


            var applicationUser = new ApplicationUser()
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                UserName = model.UserName,
                Discriminator=model.Discriminator ,
                Email=model.Email
            };

            try
            {
                       
                var result = await _usermanager.CreateAsync(applicationUser, model.Password);

                return Ok(result);

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public async Task<IActionResult> Login(Models.LoginModel model)
        {
            var user = await _usermanager.FindByNameAsync(model.UserName);
            if (user != null && await _usermanager.CheckPasswordAsync(user, model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }


    }


      
}
