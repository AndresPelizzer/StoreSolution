using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Data;
using StoreShared;

[ApiController]
[Route("api/[controller]")]
public class ClientiController : ControllerBase
{
    private readonly StoreDbContext _context;

    public ClientiController(StoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cliente>>> GetClienti()
    {
        return await _context.Clienti.ToListAsync();
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {


        Cliente? Cliente = await _context.Clienti.FindAsync(id);
        if (Cliente != null)
        {

            return Cliente;
        }
        else
        {
            return NotFound();
        }


    }
    [HttpDelete("{id}")]

    public async Task<ActionResult<Cliente>> DeleteCliente(int id)
    {
        Cliente? Cliente = await _context.Clienti.FindAsync(id);
        if (Cliente != null)
        {

            _context.Clienti.Remove(Cliente);
            await _context.SaveChangesAsync();
            return Ok(Cliente);
        }
        else
        {
            return NotFound();
        }

    }



    [HttpPost]

    public async Task<ActionResult<Cliente>> AddCliente(Cliente Cliente)
    {
        await _context.Clienti.AddAsync(Cliente);
        await _context.SaveChangesAsync();
        return Cliente;

    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Cliente>> UpdateCliente(Cliente Cliente, int id)
    {
        Cliente? Cliente_da_aggiornare = await _context.Clienti.FindAsync(id);
        if (Cliente_da_aggiornare != null)
        {
            Cliente_da_aggiornare.Nome = Cliente.Nome;
            Cliente_da_aggiornare.Cognome = Cliente.Cognome;
            Cliente_da_aggiornare.Email = Cliente.Email;
            Cliente_da_aggiornare.Settore= Cliente.Settore;




            await _context.SaveChangesAsync();
            return Cliente_da_aggiornare;
        }
        else
        {
            return NotFound();
        }


    }
}
