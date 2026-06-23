using FootballBlazor.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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
    public async Task<ActionResult<Result>> Register([FromForm] Utenti user, IFormFile? Curriculum)
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


            string password = user.Password;

            if (string.IsNullOrWhiteSpace(password))
            {
                return new Result
                {
                    Success = false,
                    Message = "Specificare la Password"
                };
            }
            if (password.Length < 6)
            {
                return new Result
                {
                    Success = false,
                    Message = "La password deve essere almeno di 6 caratteri!"
                };
            }

            if (!password.Any(c => "!@#$%^&*()_+-=[]{}|;:',.<>?/".Contains(c)))
            {
                return new Result
                {
                    Success = false,
                    Message = "La password deve contenere almeno un carattere speciale!"
                };
            }


            string encryptedPwd = this.EncryptSHA256(password);
            user.Password = encryptedPwd;

            int max_id = _context.Utenti.Any()
                ? _context.Utenti.Max(u => u.Id)
                : 0;


            user.Id = max_id + 1;
            user.DataIscrizione = DateTime.Now;
            if (Curriculum != null) {
                using var ms = new MemoryStream();
                await Curriculum.CopyToAsync(ms);
                user.Curriculum=ms.ToArray();
            }

            _context.Utenti.Add(user);
            int saved = await _context.SaveChangesAsync();

            if (saved > 0)
            {


                bool sent = InviaNotifica(user);

              

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



    private bool InviaNotifica(Utenti user)
    {
        try
        {

            // verifica utenti supervisori...
            var supervisori = _context.Utenti.Where(user => user.Ruolo == "supervisore");
            if (!supervisori.Any()) return true;

            var emails = supervisori.Select(u => u.Email).ToArray();


            var smtpSection = _cfg.GetSection("SMTP");
            string server = smtpSection.GetValue<string>("server") ?? "";
            string username = smtpSection.GetValue<string>("username") ?? "";
            string password = smtpSection.GetValue<string>("password") ?? "";
            int port = smtpSection.GetValue<int>("port");

            bool enable_ssl = smtpSection.GetValue<bool>("enable_ssl");
            string from = smtpSection.GetValue<string>("from") ?? "";


            SmtpClient smtp = new SmtpClient(server, port);
            smtp.EnableSsl = enable_ssl;

            var credentials = new NetworkCredential(username, password);
            smtp.Credentials = credentials;


            MailMessage msg = new MailMessage();
            msg.From = new System.Net.Mail.MailAddress(from);

            msg.Subject = "Nuova Iscrizione";
            msg.IsBodyHtml = true;
            msg.Body = $"<h3>Nuova iscrizione</h3>Si e' iscritto: <b>{user.Nome}</b><p>Curriculum allegato.</p>";
            if (user.Curriculum != null)
            {
                var stream = new MemoryStream(user.Curriculum);
                var attachment = new Attachment(stream, "curriculum.pdf", "application/pdf");
                msg.Attachments.Add(attachment);
            }
            foreach (var email in emails)
            {
                var address = new MailAddress(email);
                msg.To.Add(address);
            }

            smtp.Send(msg);
            return true;

        }
        catch (Exception ex)
        {
            string error = ex.Message;
            return false;
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


    
    [HttpGet]
    public async Task<ActionResult<List<Utenti>>> GetUtenti()
    {
        var listautenti= await _context.Utenti.ToListAsync();
        return Ok(listautenti);
    }
   }





