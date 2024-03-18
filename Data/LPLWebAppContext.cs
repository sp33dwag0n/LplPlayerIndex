using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LPLWebApp.Models;

namespace LPLWebApp.Data
{
    public class LPLWebAppContext : DbContext
    {
        public LPLWebAppContext (DbContextOptions<LPLWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<LPLWebApp.Models.Player> Player { get; set; } = default!;
    }
}
