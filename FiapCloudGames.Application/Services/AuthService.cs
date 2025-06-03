using FiapCloudGames.Application.Interfaces;
using FiapCloudGames.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapCloudGames.Application.Services;

public class AuthService(IConfiguration configuration) : IAuthService {
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(User user) {
        string issuer = this._configuration["Jwt:Issuer"]!;
        string audience = this._configuration["Jwt:Audience"]!;
        string key = this._configuration["Jwt:Secret"]!;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityTokenDescriptor tokenDescriptor = new() {
            Audience = audience,
            Issuer = issuer,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature),
            Subject = new([
                new(nameof(user.UserId), user.UserId.ToString()!),
                new(nameof(user.Name), user.Name),
                new(ClaimTypes.Role, user.UserType.ToString())
            ])
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
