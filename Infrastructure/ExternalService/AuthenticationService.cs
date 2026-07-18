using Application.Abstractions.ExternalService;
using Contrats.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public string? Login(LoginRequest request)
        {


            
            var user = _userRepository.GetByUserAndPassword(request);
            if (user == null)
                return string.Empty;


            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
        };

            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(keyString))
                return string.Empty;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
