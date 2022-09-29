using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testLoggers.Helpers;
using testLoggers.Models;
using testLoggers.Models.ViewModel;


using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace testLoggers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TestLoggerCTX ctx;
        private readonly IConfiguration config;

        public LoginController(TestLoggerCTX _ctx, IConfiguration _config)
        {
            ctx = _ctx;
            config = _config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(LoginVM Login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
            }

            Users User = await ctx.Users.Where(x => x.email == Login.user).FirstOrDefaultAsync();

            if (User == null)
            {
                return NotFound(ErrorHelper.Response(404, "Usuario no encontrado."));
            }

            if (HashHelper.CheckHash(Login.password, User.password, User.salt))
            {
                var secretKey = config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Login.user));
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, User.role));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);

                var response = new
                {
                    ok = true,
                    data = User,
                    token = bearer_token,
                };

                return new JsonResult(response);

            } else
            {
                return Forbid();
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            var r = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return Ok(r == null ? "" : r.Value);
        }

    }
}
