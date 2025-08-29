
namespace WebApplication2.Models
{
    public class ViewModel()
    {
       public String ?DomainName;

        public IEnumerable<ShortUrl> ListaUrls { get; set; } = new List<ShortUrl>(); // Lista de URLs
        public ShortUrl NuevaUrl { get; set; } = new  (); // Objeto para nueva URL

        public bool Ramdom { get; set; }

        public bool PublicLink { get; set; } = false;

    }
}
