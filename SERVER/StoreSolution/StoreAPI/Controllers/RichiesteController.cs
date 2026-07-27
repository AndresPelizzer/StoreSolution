using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StoreShared.Models.StoreDb;

[ApiController]
[Route("api/[controller]")]
public class RichiesteController : ControllerBase
{
    private readonly StoreDbContext _context;

    public RichiesteController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Richiesta>>?> GetRichieste()
    {
        try
        {
            var test = await _context.Richiesta.ToListAsync();
            var result = await  _context.Richiesta.Include(r => r.Area).Include(r => r.Cliente).Include(r => r.Dipendente).ToListAsync();
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Richiesta>> GetRichiesta(int id)
    {


        Richiesta? Richiesta = await _context.Richiesta.Include(r => r.Area).Include(r => r.Cliente).Include(r => r.Dipendente).FirstOrDefaultAsync(r => r.Codice == id);
        if (Richiesta != null)
        {

            return Richiesta;
        }
        else
        {
            return NotFound();
        }


    }
    [HttpDelete("{id}")]

    public async Task<ActionResult<Richiesta>> DeleteRichiesta(int id)
    {
        Richiesta? Richiesta = await _context.Richiesta.FindAsync(id);
        if (Richiesta != null)
        {

            _context.Richiesta.Remove(Richiesta);
            await _context.SaveChangesAsync();
            return Ok(Richiesta);
        }
        else
        {
            return NotFound();
        }

    }



    [HttpPost]

    public async Task<ActionResult<Richiesta>> AddRichiesta(Richiesta Richiesta)
    {

        if(Richiesta.CodiceCliente==null || Richiesta.CodiceCliente == 0)
        {
            return BadRequest("Codice Cliente Obbligatorio");
        }



        await _context.Richiesta.AddAsync(Richiesta);
        await _context.SaveChangesAsync();
        return Richiesta;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Richiesta>> UpdateRichiesta(Richiesta Richiesta, int id)
    {
        Richiesta? Richiesta_da_aggiornare = await _context.Richiesta.FindAsync(id);
        if (Richiesta_da_aggiornare != null)
        {
            Richiesta_da_aggiornare.Titolo= Richiesta.Titolo;
            string? vecchiostato = Richiesta_da_aggiornare.Stato;

            if (Richiesta.Stato == "Conclusa" && vecchiostato!="Conclusa")
            {
                var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Codice == Richiesta.CodiceCliente);

                Notifica noti = new Notifica
                {
                    Messaggio = "La tua richiesta è stata conclusa!",
                    Letta = false,
                    DataCreazione = DateTime.Now,
                    CodiceCliente = cliente!.Codice
                };
                _context.Notifica.Add(noti);


            }


            


            Richiesta_da_aggiornare.Stato = Richiesta.Stato;

            Richiesta_da_aggiornare.Descrizione = Richiesta.Descrizione;
            Richiesta_da_aggiornare.DataRichiesta = Richiesta.DataRichiesta;
            Richiesta_da_aggiornare.CodiceDipendente = Richiesta.CodiceDipendente;
            Richiesta_da_aggiornare.CodiceCliente= Richiesta.CodiceCliente;

            Richiesta_da_aggiornare.CodiceArea = Richiesta.CodiceArea;

            await _context.SaveChangesAsync();
            return Richiesta_da_aggiornare;
        }
        else
        {
            return NotFound();
        }





    }



    [HttpPut("{id}/allegato")]

    public async Task<ActionResult> UploadAllegato(int id, IFormFile file)
    {
        var richiesta = await _context.Richiesta.FindAsync(id);

        if (richiesta == null)
        {
            return NotFound();
        }

        var cartella = Path.Combine("wwwroot", "allegati");
        Directory.CreateDirectory(cartella);



        var nomeFile = $"richiesta_{id}_{file.FileName}";
        var percorso = Path.Combine(cartella, nomeFile);

        using(var stream = new FileStream(percorso, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }


        richiesta.Allegato = nomeFile;

        await _context.SaveChangesAsync();

        return Ok(nomeFile);
    }



    [HttpPut("stream")]
    public async Task<IActionResult> Stream()
    {
        try
        {
            // Put your code here

            using (var ms = new MemoryStream())
            {
                await Request.Body.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();

                return Ok(new { Completed = true, fileSize = fileBytes.Length });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }





}






