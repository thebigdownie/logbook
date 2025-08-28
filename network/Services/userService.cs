using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoAuthenticatorAPI.Models;
using network.Models;
using network.R;

namespace network.Services
{
    public interface IUserService
    {
        Task<UserViewModel?> LoginAsync(UserLoginRequest request);
        Task<UserViewModel?> RegisterAsync(UserRegistrationRequest request);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<UserViewModel?> LoginAsync(UserLoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return null;

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword) return null;

            var token = GenerateJwtToken(user);

            return new UserViewModel
            {
                UserId = user.Id.ToString(),
                Username = user.UserName!,
                Trips = user.Trips,
                Friends = user.Friends,
                Following = user.Following,
                AccessToken = token
            };
        }

        public async Task<UserViewModel?> RegisterAsync(UserRegistrationRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null) return null;

            var newUser = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Username,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!createUserResult.Succeeded) return null;

            var token = GenerateJwtToken(newUser);

            return new UserViewModel
            {
                UserId = newUser.Id.ToString(),
                Username = newUser.UserName!,
                Trips = [],
                Friends = [],
                Following = [],
                AccessToken = token
            };
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("7c3064cb6666e1599c3e074282578dd60926da34ca3376873f70990f07b4a829" ?? throw new InvalidOperationException("Jwt:Key not configured"))
            );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse("30"));

            var token = new JwtSecurityToken(
                issuer: _config["http://localhost:5074/"],
                audience: _config["http://localhost:5074/"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
