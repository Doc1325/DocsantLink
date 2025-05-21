using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ShortDBContext : DbContext
    {
        public ShortDBContext (DbContextOptions<ShortDBContext> options)
            : base(options)
        {
            
        }

        public DbSet<WebApplication2.Models.ShortUrl> ShortUrl { get; set; } = default!;



    }
}
