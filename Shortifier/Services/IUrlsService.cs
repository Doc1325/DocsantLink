using WebApplication2.Models;

namespace Shortifier.Services
{
    public interface IUrlsService
    {
        public List<String> ErrorList { get; }
        Task<IEnumerable<ShortUrl>> GetAllUrls();

        Task<string> GetUrl(string  hash);
        Task ShortUrl (ShortUrl newShort);

        bool IsValidUrl(string url);



    }
}
