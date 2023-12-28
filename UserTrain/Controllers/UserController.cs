using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserTrain.Data;
using Microsoft.AspNetCore.Http;
using UserTrain.Model;

namespace UserTrain.Controllers
{
    [Route("http://www.iranbarnamenevis.ir/")]

    [ApiController]
    public class UserController : ControllerBase
    {
        private Context _context;
        IConfiguration _configuration;
        public UserController(Context context,IConfiguration configuration)
        {
            _configuration = configuration;
            _context=context;   
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(User login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user=_context.users.FirstOrDefault(n=>n.Email==login.Email &&n.Password==login.Password);
            if (user==null)
            {
                return Unauthorized();
            }
            

                var securityKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                    );
                var signingCredentials = new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256
                    );
                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("userId", user.Id.ToString()));
                claimsForToken.Add(new Claim("Email", user.Email.ToString()));
                //claimsForToken.Add(new Claim(ClaimTypes.Email, user.City.ToString()));

                var jwtSecurityToke = new JwtSecurityToken(
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claimsForToken,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    signingCredentials
                    );

                var tokenToReturn = new JwtSecurityTokenHandler()
                    .WriteToken(jwtSecurityToke);
                return Ok(tokenToReturn);
         
        }
    }
}
