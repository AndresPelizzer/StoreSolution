using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreShared.Models.StoreDb;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RichiesteFerieController : ControllerBase
{
    private readonly StoreDbContext _context;

    public RichiesteFerieController(StoreDbContext context)
    {

        _context = context;




    }

    [HttpGet]

    [HttpGet]
    public async Task<ActionResult<List<RichiestaFerie>>> GetFerie()
    {
        return await _context.RichiesteFerie
            .Include(f => f.Dipendente) 
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RichiestaFerie>> GetFeria(int id)
    {
        var feria = await _context.RichiesteFerie.FindAsync(id);
        return feria!;
    }

    [HttpPost]
    public async Task<ActionResult<RichiestaFerie>> AddFeria([FromBody] RichiestaFerie feria)
    {

        feria.Stato = "In Attesa";
        await _context.RichiesteFerie.AddAsync(feria);
        await _context.SaveChangesAsync();

        var dipendente = await _context.Dipendente.FirstOrDefaultAsync(d => d.Codice == feria.CodiceDipendente);
        if (dipendente != null && dipendente.CodiceAreaAppl != null)
        {
            var capoArea = await _context.Dipendente.FirstOrDefaultAsync(d => d.CodiceAreaAppl == dipendente.CodiceAreaAppl && d.CapoArea == true);
            if (capoArea != null)
            {
                var notifica = new Notifica
                {
                    Letta = false,
                    Messaggio = $"Nuova richiesta di ferie ricevuta da {dipendente.Nome} {dipendente.Cognome} (ID: {dipendente.Codice})",
                    CodiceDipendente = capoArea.Codice,
                    DataCreazione = DateTime.Now,
                    CodiceCliente = null

                };
                _context.Notifica.Add(notifica);
                await _context.SaveChangesAsync();
            }
        }

        return Ok(feria);
    }

    [HttpDelete("{id}")]
    public async Task DeleteFeria(int id)
    {
        var feria = await _context.RichiesteFerie.FindAsync(id);
        _context.RichiesteFerie.Remove(feria!);
        await _context.SaveChangesAsync();

    }


    [HttpPut("{id}")]
    public async Task<ActionResult<RichiestaFerie>> UpdateFeria(int id, RichiestaFerie feria)
    {
        var fer = await _context.RichiesteFerie.FindAsync(id);
        fer!.Note = feria.Note;
        fer.Stato = feria.Stato;
        fer.CodiceDipendente = feria.CodiceDipendente;

        fer.DataInizio = feria.DataInizio;
        fer.DataFine = feria.DataFine;
        await _context.SaveChangesAsync();

        return Ok(fer);
    }



    [HttpGet("dipendente/{id}")]
    public async Task<ActionResult<List<RichiestaFerie>>> GetFerieDipendente(int id)
    {
        return await _context.RichiesteFerie
            .Where(f => f.CodiceDipendente == id && f.Stato == "In Attesa")
            .ToListAsync();
    }

    [HttpPut("{id}/stato")]
    public async Task<ActionResult> AggiornaStato(int id, [FromBody] string stato)
    {
        var feria = await _context.RichiesteFerie.FindAsync(id);
        if (feria == null) return NotFound();
        feria.Stato = stato.Replace("\"", "").Trim();
        await _context.SaveChangesAsync();
        return Ok();
    }
}


