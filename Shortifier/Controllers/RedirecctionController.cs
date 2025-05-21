using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shortifier.Services;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("/")]
    [ApiController]
    public class RedirecctionController : ControllerBase
    {
        private IUrlsService _urlsService;


        public RedirecctionController (IUrlsService urlsService)
        {
            _urlsService = urlsService;
        }


        [HttpGet("{name}")]
        public async  Task<IActionResult> GetAsync(string name) {
        var LinkToRedirect = await _urlsService.GetUrl(name);

            if (LinkToRedirect == null) { 
            return Redirect("/NotFound");
            }
            return Redirect(LinkToRedirect);
        }
    }
}
