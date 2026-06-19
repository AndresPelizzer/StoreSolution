using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            string username = credenziali.Username;
            string password = credenziali.Password;

            var user = await _context.Utenti.FirstOrDefaultAsync(u => u.Username == username && u.Password == this.EncryptSHA256(password));
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


    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<Result>> Register([FromBody] Utenti user)
    {
        try
        {

            // controlli....

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                return new Result
                {
                    Success = false,
                    Message = "Specificare il nome utente"
                };
            }

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new Result
                {
                    Success = false,
                    Message = "Specificare la Password"
                };
            }

            string password = user.Password;
            string encryptedPwd = this.EncryptSHA256(password);
            user.Password = encryptedPwd;



            int max_id = _context.Utenti.Any()
                ? _context.Utenti.Max(u => u.Id)
                : 0;


            user.Id = max_id + 1;



            _context.Utenti.Add(user);
            int saved = await _context.SaveChangesAsync();


            if (saved > 0)
            {
                return new Result
                {
                    Success = true,
                    Message = "Registrazione avvenuta con successo"
                };
            }
            else
            {
                return new Result
                {
                    Success = false,
                    Message = "Salvataggio non riuscito"
                };
            }

        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return new Result
            {
                Success = false,
                Message = error
            };
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






    private string EncryptSHA256(string input)
    {
        try
        {
            // Converte la stringa in un array di byte usando la codifica UTF-8
            byte[] dati = Encoding.UTF8.GetBytes(input);

            // Calcola l'hash SHA256 in un'unica riga
            byte[] hashBytes = SHA256.HashData(dati);

            // Converte l'array di byte in una stringa esadecimale
            string hashString = Convert.ToHexString(hashBytes);
            return hashString;
        }
        catch
        {
            return string.Empty;
        }
    }







}