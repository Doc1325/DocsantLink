using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shortifier.Services;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class UrlsService: IUrlsService
    {
        private readonly ShortDBContext _context;
        public List<String> ErrorList { get; }
        public UrlsService(ShortDBContext context)
        {
            _context = context;
            ErrorList = new List<String>();
        }



        public bool IsValidUrl(string url)
        {
            if (url.IsNullOrEmpty())
            {
                ErrorList.Add("La url no puede estar vacia");
                return false;


            }



            Uri result;
            bool isValid = Uri.TryCreate(url, UriKind.Absolute, out result);
            if (isValid && result.IsWellFormedOriginalString())
            {
                return true;
            }
            else
            {
                ErrorList.Add("La url Es invalida");
                return false;

            }
        }

        public async Task<string> GetUrl(string hash)
        {
            
            var LinksList = await _context.ShortUrl.ToListAsync();
             
             String? LinkToRedirect =  LinksList.Where(l => String.Equals(l.Name, hash, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Url;
               
            
            return LinkToRedirect;
        }
        public async Task<IEnumerable<ShortUrl>> GetAllUrls()
        {
            IEnumerable<ShortUrl> UrlList = await _context.ShortUrl.ToListAsync();
            return UrlList.Reverse();// de esta forma logro que los nuevos elementos aparezcan al inicio


        }


        public async Task ShortUrl(ShortUrl newShort)
        {
             newShort.Name = newShort.Name.Trim().Replace(" ", "-");
            _context.ShortUrl.Add(newShort);
            await _context.SaveChangesAsync();
           

        }

       
    }

}
