using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrlShortener_Backend.Models;

namespace UrlShortener_Backend.Services;

public class JwtHandler
{
    private static string? _keyString;

    public static void Initialize(IConfiguration config)
    {
        _keyString = config["JwtHandler:Key"].Trim();
    }
    public static string? CreateToken(User user, IConfiguration config)
    {
        if (_keyString.IsNullOrEmpty()) throw new ArgumentException("Encoding key was null of empty");

        List<Claim> claims = new()
        {
            new Claim("id", user.UserId.ToString()),
            new Claim("login", user.Login),
            new Claim("role", user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyString));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtObj = new JwtSecurityToken(
            issuer: config["JwtSettings:Issuer"],
            audience: config["JwtSettings:Audience"],
            notBefore: DateTime.UtcNow.AddMinutes(-1),
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
            signingCredentials: credentials);
        var jwt = new JwtSecurityTokenHandler().WriteToken(jwtObj);

        return jwt;
    }
}