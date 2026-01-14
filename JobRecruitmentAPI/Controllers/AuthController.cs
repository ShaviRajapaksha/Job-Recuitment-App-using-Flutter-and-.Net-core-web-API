using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobRecruitmentAPI.Data;
using JobRecruitmentAPI.DTOs;
using JobRecruitmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace JobRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            // Registration logic here
            //check if user exists, hash password, save user to DB, etc.
            if(await _context.Users.AnyAsync(u=>u.Email == registerDto.Email))
            {
                return BadRequest("User already exists.");
            }
            //create a new user
            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                UserType = registerDto.UserType,
                Phone = registerDto.Phone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //generate token
            var token = GenerateJwtToken(user);

            return Ok( new AuthResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserType = user.UserType,
                Token = token
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");

            //generate token
            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                UserType = user.UserType,
                Token = token
            });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "default_secret_key_here_change_in_production"));
            
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.UserType)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}