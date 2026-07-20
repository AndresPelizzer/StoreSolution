using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreShared.Models.StoreDb;

namespace StoreAPI.Controllers
{
    public class SediController : ControllerBase
    {
        private readonly StoreDbContext _context;
       public SediController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<List<Sede>> GetSedi()
        {
            return await _context.Sede.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sede>> GetSede(int id)
        {
            var sede = await _context.Sede.FindAsync(id);
            if (sede != null)
            {
                return sede;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]

        public async Task<ActionResult<Sede>> AddSede([FromBody]Sede sede)
        {

            if (sede != null)
            {
                 await _context.Sede.AddAsync(sede);
                await _context.SaveChangesAsync();
                return sede;
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Sede>> DeleteSede(int id)
        {
            var SedeDaEliminare = await _context.Sede.FindAsync(id);
            if(SedeDaEliminare != null)
            {
                _context.Sede.Remove(SedeDaEliminare);
                await _context.SaveChangesAsync();
                return SedeDaEliminare;
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Sede>> UpdateSede(int id,Sede sede)
        {
            Sede? SedeDaAggiornare = await _context.Sede.FindAsync(id);
            if (SedeDaAggiornare != null)
            {
                SedeDaAggiornare.Nome= sede.Nome;
                SedeDaAggiornare.Citta = sede.Citta;
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return SedeDaAggiornare;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
