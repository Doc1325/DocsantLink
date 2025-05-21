using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shortifier.Services;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Views.Home
{
    public class ShortUrlsController : Controller
    {
        private readonly IConfiguration _configuration;
        private IUrlsService _urlsService;
        public ShortUrlsController(IConfiguration config, IUrlsService urlsService)
        {
            _configuration = config;
            _urlsService = urlsService;
        }

        public async Task<IActionResult> Index()
        {

            var modelo = new ViewModel
            {
                ListaUrls = await _urlsService.GetAllUrls(),
                NuevaUrl = new ShortUrl(),
                DomainName = _configuration["DomainName"]
            };

            return View("Index", modelo);
        }
        [Route("NotFound")]
        public IActionResult NotFound()
        {
            return View(); // Views/Home/Error404.cshtml
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViewModel model)
        {
           

            model.ListaUrls = await _urlsService.GetAllUrls(); 

            if (_urlsService.IsValidUrl(model.NuevaUrl.Url) == false)
            {
                ModelState.AddModelError("NuevaUrl.Url", _urlsService.ErrorList.First());
                model.DomainName = _configuration["DomainName"];

                return View("Index", model);


            }

            if(String.Equals( model.NuevaUrl.Name, "NotFound", StringComparison.OrdinalIgnoreCase))
            {

                ModelState.AddModelError("NuevaUrl.Name", "Lo siento, este hash es invalido.");
                model.DomainName = _configuration["DomainName"];
                return View("Index", model);

            }

            if (model.NuevaUrl.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("NuevaUrl.Name", "El campo nombre no puede estar vacio");
                model.DomainName = _configuration["DomainName"];

                return View("Index", model);
            }
            if (model.ListaUrls.Any(s => String.Equals(s.Name, model.NuevaUrl.Name, StringComparison.OrdinalIgnoreCase) ))
            {
                ModelState.AddModelError("NuevaUrl.Name", "Este nombre ya está registrado.");
                model.DomainName = _configuration["DomainName"];
               
                return View("Index", model); 
            }

            if (ModelState.IsValid)
            {
                
                 await _urlsService.ShortUrl(model.NuevaUrl);

                return RedirectToAction("Index");

            }

            return NoContent();

           
        }
 

   
    }
}
