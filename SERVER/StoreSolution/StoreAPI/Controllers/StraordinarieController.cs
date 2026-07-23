using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreShared.Models.StoreDb;



namespace StoreAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StraordinarieController : ControllerBase
    {
        private readonly StoreDbContext _context;



        public StraordinarieController(StoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<List<Straordinaria>> GetStraordinarie()
        {
            return await _context.Straordinaria.Include(s=>s.Dipendente).ToListAsync();
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<Straordinaria>> GetStraordinaria(int id)
        {
            var straordinaria = await _context.Straordinaria.Include(s=>s.Dipendente).FirstOrDefaultAsync(s=>s.Codice==id);

            if (straordinaria != null)
            {
                return straordinaria;
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]

        public async Task<ActionResult<Straordinaria>> PostStraordinaria(Straordinaria straordinaria)
        {



            await _context.Straordinaria.AddAsync(straordinaria);

            var dipendente = await _context.Dipendente.FirstOrDefaultAsync(d => d.Codice == straordinaria.CodiceDipendente );
            var capoArea = await _context.Dipendente.FirstOrDefaultAsync(d => d.CodiceAreaAppl == dipendente!.CodiceAreaAppl && d.CapoArea == true);

            var notifica = new Notifica
            {
                Messaggio=$"Richiesta di straordinari da parte di {dipendente?.Nome}",
                Letta=false,
                DataCreazione=DateTime.Now,
                CodiceCliente=null,
                CodiceDipendente=capoArea!.Codice,
                
            };


            await _context.Notifica.AddAsync(notifica);


            await _context.SaveChangesAsync();
            return Ok(straordinaria);

        }

        [HttpPut("{id}")]


        public async Task<ActionResult<Straordinaria>> PutStraordinaria(Straordinaria straordinaria, int id)
        {

            var str = await _context.Straordinaria.FindAsync(id);
            if(str != null)
            {
                str.NumeroOre = straordinaria.NumeroOre;
                str.DataInizio = straordinaria.DataInizio;
                str.DataFine= straordinaria.DataFine;
                str.Stato = straordinaria.Stato;
                await _context.SaveChangesAsync();
                return Ok(str);
               
            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteStraordinaria(int id)
        {
            var straordinaria = await _context.Straordinaria.FindAsync(id);
            if (straordinaria != null)
            {
                _context.Straordinaria.Remove(straordinaria!);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
