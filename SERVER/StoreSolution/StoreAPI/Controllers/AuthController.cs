
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreShared.Models;
using StoreShared.Models.StoreDb;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;


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

        try
        {

            var utente = await _context.Utente.FirstOrDefaultAsync(u => u.Username == credenziali.Username);
            
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

                    var dipendente = await _context.Dipendente
                            .FirstOrDefaultAsync(d => d.Codice == utente.CodiceDipendente);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(new LoginResponse
                    {
                        Token = tokenString,
                        Ruolo = utente.Ruolo,
                        CodiceUtente = utente.Codice,
                        IsCapoArea = dipendente?.CapoArea

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
        catch(Exception ex)
        {

            return new LoginResponse();
        }







        

    }
}

