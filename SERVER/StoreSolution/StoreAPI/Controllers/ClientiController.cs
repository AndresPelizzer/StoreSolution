using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using StoreAPI.Data;
using StoreShared.Models;

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

        if (string.IsNullOrWhiteSpace(Cliente.PartitaIva))
        {
            return BadRequest("La partita IVA esiste già");
        }
        bool esistegia = await _context.Clienti.AnyAsync(c => c.PartitaIva == Cliente.PartitaIva);
        if (esistegia)
        {
            return BadRequest($"Esiste già un cliente con questa partita IVA({Cliente.PartitaIva})");
        }
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
            Cliente_da_aggiornare.Settore = Cliente.Settore;




            await _context.SaveChangesAsync();
            return Cliente_da_aggiornare;
        }
        else
        {
            return NotFound();
        }


    }



    [HttpPost]
    [Route("import")]
    public async Task<ActionResult> ImportClientiExcel()
    {
        int processed = 0;
        int inserted = 0;

        var file = Request.Form.Files[0];
        if (file == null || file.Length == 0)
            return BadRequest("Nessun file ricevuto");

        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            using (var package = new ExcelPackage(ms))
            {
                var sheet = package.Workbook.Worksheets[0];
                int lastRow = sheet.Dimension.End.Row;

                for (int row = 2; row <= lastRow; row++) 
                {
                    processed++;

                    string nome = sheet.Cells[row, 1].Text;
                    string cognome = sheet.Cells[row, 2].Text;
                    string email = sheet.Cells[row, 3].Text;
                    string settore = sheet.Cells[row, 4].Text;
                    string partitaIva = sheet.Cells[row, 5].Text;

                    if (string.IsNullOrWhiteSpace(partitaIva))
                    {
                        continue;
                    }

                    bool esisteGia = await _context.Clienti.AnyAsync(c => c.PartitaIva == partitaIva);
                    if (esisteGia)
                    {
                       
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cognome))
                        continue; 

                    var cliente = new Cliente
                    {
                        Nome = nome,
                        Cognome = cognome,
                        Email = email,
                        Settore = settore,
                        PartitaIva=partitaIva
                    };

                    _context.Clienti.Add(cliente);
                    inserted++;
                }

                await _context.SaveChangesAsync();
            }

            return Ok(new ImportResult { Processed = processed, Inserted = inserted });
        }


      



    }

    [HttpPost]
    [Route("import-csv")]

    public async Task<ActionResult> ImportClienticsv()
    {
        int processed = 0;
        int inserted = 0;
        var file = Request.Form.Files[0];

        if(file==null || file.Length == 0)
        {
            return BadRequest("Nessun File ricevuto");
        }


        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            string? headerline = await reader.ReadLineAsync();


            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                processed++;
                var values= line.Split(',');

                if (values.Length < 5)
                {
                    continue;
                }

                string nome = values[0].Trim();
                string cognome = values[1].Trim();
                string email= values[2].Trim();
                string settore= values[3].Trim();
                string partitaIva = values[4].Trim();

                if (string.IsNullOrWhiteSpace(partitaIva))
                {
                    continue;
                }

                bool esistegia= await _context.Clienti.AnyAsync(c=>c.PartitaIva== partitaIva );

                if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cognome))
                    continue;

                var cliente = new Cliente
                {
                    Nome = nome,
                    Settore = settore,
                    Cognome = cognome,
                    Email = email,
                    PartitaIva = partitaIva
                };

                await _context.Clienti.AddAsync(cliente);
                inserted++;


            }
            await _context.SaveChangesAsync();

        }
        return Ok(new ImportResult { Processed = processed, Inserted = inserted });
    }

}
