using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Ticket_Management.DBContext;
using Ticket_Management.Entity.Model;
using Ticket_Management.Entity.Request;
using Ticket_Management.PlatformException;
using Ticket_Management.Repositories.IRepositories;
using Ticket_Management.Utility;

namespace Ticket_Management.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration _configuration;
        public UserRepository(AppDbContext context, IConfiguration configuration)
        {
            appDbContext = context;
            _configuration = configuration;
        }
        public async Task<string> CreateUser(UserRequest request)
        {
            User user = await appDbContext.Users.Where(user => user.UserName == request.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                user = new User
                {
                    UserName = request.UserName,
                    RoleId = request.RoleId,
                    Password = EncrytionUtility.Base64Encode(request.Password)
                };

                await appDbContext.Users.AddAsync(user);
                await appDbContext.SaveChangesAsync();
            }
            return "Created";
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IQueryable<User> users = appDbContext.Users.AsQueryable();       
            return await users.OrderByDescending(t => t.Id).ToListAsync();
        }

        public async Task<string> Login(LoginRequest loginRequest)
        {
            var encodedPassword = EncrytionUtility.Base64Encode(loginRequest.Password);

            var user = await appDbContext.Users
                .FirstOrDefaultAsync(x =>
                    x.UserName.ToLower() == loginRequest.UserName.ToLower() &&
                    x.Password == encodedPassword);

            if (user == null)
                throw new Exception("Invalid Credentials");

            string role = user.RoleId == 1 ? "Admin" : "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", user.Id.ToString())
            };

            var duration = Convert.ToDouble(_configuration["Jwt:DurationInMinutes"]);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
