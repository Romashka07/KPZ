using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Lab3KPZ_CF_.Data;
using Lab3KPZ_CF_.ViewModels;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            var role = await _context.Roles.FindAsync(model.RoleID);
            if (role == null)
                return BadRequest("Invalid role selected.");

            var user = new User
            {
                Username = model.Username,
                PasswordHash = model.Password, // Note: Hash the password in production
                Email = model.Email,
                FirstName = model.FirstName,
                SecondName = model.SecondName,
                RoleID = model.RoleID
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.PasswordHash == model.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { message = "Login successful", userRole = user.Role.RoleName });
        }

    }
}
