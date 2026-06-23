
using Microsoft.EntityFrameworkCore;
using StoreShared.Models;

namespace StoreAPI.Data
{
    // Crea la classe del database (StoreDbContext) che eredita (: DbContext) tutti i superpoteri da Microsoft
    public class StoreDbContext : DbContext
    {
        // IL COSTRUTTORE: Riceve la scatola delle configurazioni (stringa di connessione, tipo di DB)
        // creata da ASP.NET Core nel file Program.cs.
        // <StoreDbContext> specifica che queste opzioni sono bloccate e destinate solo a questo database.
        // : base(options) prende questa variabile e la spedisce "al piano di sopra" alla classe madre di Microsoft.
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
          
        }

     

   
        public DbSet<Cliente> Clienti { get; set; }

        
        public DbSet<Richiesta> Richieste { get; set; }

        
        public DbSet<Area> Aree { get; set; }

        
        public DbSet<Dipendente> Dipendenti { get; set; }
    }
}
