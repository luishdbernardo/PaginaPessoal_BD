using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaginaPessoal_BD.Models;

namespace PaginaPessoal_BD.Data
{
    public class PaginaPessoalBDContext : DbContext
    {
        public PaginaPessoalBDContext (DbContextOptions<PaginaPessoalBDContext> options)
            : base(options)
        {
        }

        public DbSet<PaginaPessoal_BD.Models.Experiencia> Experiencia { get; set; }
    }
}
