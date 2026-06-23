using System.ComponentModel.DataAnnotations;

namespace StoreShared
{
    public class Cliente
    {


        [Key]
        public int Codice { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
        public string? Email { get; set; }
        public string? Settore { get; set; }

    }
}
