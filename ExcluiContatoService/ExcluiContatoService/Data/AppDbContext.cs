using ExcluiContatoService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExcluiContatoService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ContatoModel> CONTATO_CTT { get; set; }
    }
}
