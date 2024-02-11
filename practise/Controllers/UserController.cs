using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using practise.DTO;
using practise.Entitys;
using practise.model;
using practise.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace practise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
            this._logger = logger;
        }
        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> Users = userService.GetUsers();
                
                return StatusCode(200, Users);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost, Route("Register")]
        [AllowAnonymous]
        
        public IActionResult AddUser([FromBody] UserDTO usersDTO)
        {
            try
            {
                User user = _mapper.Map<User>(usersDTO);
                userService.AddUser(user);
                return StatusCode(200, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [HttpPut, Route("EditUser")]
        [Authorize(Roles = "User")]
        public IActionResult EditUser([FromBody] UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                userService.UpdateUser(user);
                return StatusCode(200, user);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete, Route("DeleteUser/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                userService.DeleteUser(userId);
                return StatusCode(200, new JsonResult($"User with Id {userId} is Deleted"));
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate([FromBody] Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthResponse authResponse = new AuthResponse();
                if (user != null )
                {
                    authResponse.UserName = user.UserName;
                    authResponse.Role = user.Role;
                    authResponse.UserId = user.UserId;
                    authResponse.Token = GetToken(user);
                }
                return StatusCode(200, authResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
        private string GetToken(User? user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim(ClaimTypes.Email,user.Email)
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }

}
