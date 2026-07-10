namespace StoreBlazor.Services
{
    public class AuthState
    {
        public string? Token { get; set; }
        public string? Ruolo { get; set; }
        public int? CodiceUtente { get; set; }

        public bool IsCapoArea { get; set; }

        public bool BenvenutoMostrato { get; set; } = false;


    }
}
