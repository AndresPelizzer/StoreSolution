
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Data;
using StoreShared.Models;
using System.Formats.Tar;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[ApiController]
[Route("api/[controller]")]


    public class AuthController : ControllerBase
    {

    private readonly StoreDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(StoreDbContext context, IConfiguration configuration)

    {
        _context = context;
        _configuration = configuration;
    }


   

    [HttpPost("login")]





    public async Task<ActionResult<LoginResponse>> Login(Credenziali credenziali)
    {
        var utente = await _context.Utenti.FirstOrDefaultAsync(u => u.Username == credenziali.Username);
        if (utente != null)
        {
            bool passwordCorretta = BCrypt.Net.BCrypt.Verify(credenziali.Password, utente.PasswordHash);
            if (passwordCorretta)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                  new Claim(ClaimTypes.Name, utente.Username!),
                  new Claim(ClaimTypes.Role, utente.Ruolo!),
                  new Claim("CodiceUtente", utente.Codice.ToString())
};

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(8),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new LoginResponse
                {
                    Token = tokenString,
                    Ruolo = utente.Ruolo,
                    CodiceUtente = utente.Codice
                });
            }
            else
            {
                return Unauthorized();
            }
        }
        else
        {
            return Unauthorized();
        }

    }
}

