using Entertainment.DataAccess.Dtos;
using Entertainment.DataAccess.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace Entertainment.API.Controllers
{
    public class AccountController : ApiController
    {
        private readonly string secretKey;
        private readonly IAccountRepository accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            secretKey = WebConfigurationManager.AppSettings["SecretKey"];
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null || !ModelState.IsValid)
                {
                    return Content(HttpStatusCode.BadRequest, new { Message = "Enter username and password" });
                }

                UserDto userDto = await accountRepository.GetUserByUsernameAndPassword(loginDto.UserName, loginDto.Password);

                if (userDto == null || (userDto.login_user_id == 0))
                {
                    return Content(HttpStatusCode.BadRequest, new { Message = "Invalid username or password" });
                }

                var menuListDtos = await accountRepository.GetUserMenuList(Convert.ToInt64(userDto.group_id));

                var claims = new[]
                {
                    new Claim("login_user_id", userDto.login_user_id.ToString()),
                    new Claim("login_user_name", userDto.login_user_name),
                    new Claim("login_user_full_name", userDto.login_user_full_name),
                    new Claim("group_id", userDto.group_id),
                    new Claim("group_name", userDto.group_name),
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = signinCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { Token = tokenHandler.WriteToken(token), Menu = menuListDtos });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
