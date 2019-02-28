using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.API.Dtos;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        public IConfiguration config { get; }

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.config = config;
            this.repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            //validate request

            userToRegisterDto.userName = userToRegisterDto.userName.ToLower();
            if (await this.repo.UserExists(userToRegisterDto.userName))
            {
                return BadRequest("Username already exists");
            }

            var userToCreate = new User
            {
                UserName = userToRegisterDto.userName
            };

            var createdUser = await this.repo.Register(userToCreate, userToRegisterDto.password);
            // return CreatedAtRoute()
            return StatusCode(201);
            

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserToLoginDto userToLoginDto)
        {


            //throw new System.Exception("Computer says no");
            var userFromRepo = await this.repo.Login(userToLoginDto.userName.ToLower(), userToLoginDto.password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]{

                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.UserName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHendler = new JwtSecurityTokenHandler();
            var token = tokenHendler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHendler.WriteToken(token)
            });



        }
    }
}