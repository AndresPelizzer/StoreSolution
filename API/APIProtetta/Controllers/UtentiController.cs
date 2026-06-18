using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]


public class UtentiController : ControllerBase
{
    private readonly CampionatoContext _context;
    private readonly IConfiguration _cfg;

    public UtentiController(CampionatoContext context, IConfiguration cfg)
    {
        _context = context;
        _cfg = cfg;
    }



    [AllowAnonymous]
    [HttpPost]

    public async Task<ActionResult<string>> Login(Credenziali credenziali)
    {
        try
        {
            var user = await _context.Utenti.FirstOrDefaultAsync(u => u.Username == credenziali.Username && u.Password == credenziali.Password);
            if (user == null)
            {
                return string.Empty;
            }
            else
            {
                return GetToken(user);
            }
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return string.Empty;
        }
    }


    private string GetToken(Utenti user)
    {

        try
        {
            string SECRET_KEY = _cfg.GetValue<string>("SECRET_KEY") ?? "";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            int id = user.Id;
            string name = user.Nome;
            string email = user.Email;
            string role = user.Ruolo;

            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return string.Empty;
        }


    }



}