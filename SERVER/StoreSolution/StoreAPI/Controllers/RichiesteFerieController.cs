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

    public async Task<ActionResult<List<RichiestaFerie>>> GetFerie()
    {
        return await _context.RichiesteFerie.ToListAsync();
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

        var dipendente= await _context.Dipendente.FirstOrDefaultAsync(d=>d.Codice==feria.CodiceDipendente);
        if(dipendente!=null && dipendente.CodiceAreaAppl != null)
        {
            var capoArea = await _context.Dipendente.FirstOrDefaultAsync(d => d.CodiceAreaAppl == dipendente.CodiceAreaAppl && d.CapoArea == true);
            if (capoArea != null)
            {
                var notifica = new Notifica
                {
                    Letta = false,
                    Messaggio = $"Nuova richiesta di ferie ricevuta da {dipendente.Nome} {dipendente.Cognome}",
                    CodiceDipendente=capoArea.Codice,
                   
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
    public async Task<ActionResult<RichiestaFerie>>UpdateFeria(int id, RichiestaFerie feria)
    {
        var fer=  await _context.RichiesteFerie.FindAsync(id);
        fer!.Note= feria.Note;
        fer.Stato= feria.Stato;
        fer.CodiceDipendente = feria.CodiceDipendente;
       
        fer.DataInizio= feria.DataInizio;
        fer.DataFine= feria.DataFine;
        await _context.SaveChangesAsync();

        return Ok(fer);
    }
}

