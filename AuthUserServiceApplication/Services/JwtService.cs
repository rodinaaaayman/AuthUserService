using AuthUserServiceApplication.Interfaces;
using AuthUserServiceDomain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(Users User)
    {
        var claims = new[]
        {
             new Claim(
                 ClaimTypes.NameIdentifier,
                 User.Id.ToString()),
            new Claim(
                ClaimTypes.Name,
                User.Username ),
            new Claim(
                ClaimTypes.Role,
                User.Role.ToString())

    };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:AccessTokenMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }
}
