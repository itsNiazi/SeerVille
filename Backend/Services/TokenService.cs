using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _signingKey;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.GivenName, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var creds = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);

    }
}
