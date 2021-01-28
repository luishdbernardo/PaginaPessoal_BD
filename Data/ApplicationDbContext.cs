using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PaginaPessoal_BD.Models;
using Microsoft.AspNetCore.Identity;

namespace PaginaPessoal_BD.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private const string NOME_UTILIZADOR_ADMINISTRADOR_PADRAO = "admin@luisbernardo.pt";
        private const string PASSWORD_UTILIZADOR_ADMIN_PADRAO = "Qwerty_2";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PaginaPessoal_BD.Models.Experiencia> Experiencia { get; set; }

        internal static async System.Threading.Tasks.Task InsereAdministradorPadraoAsync(UserManager<IdentityUser> gestorUtilizadores)
        {

            IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(NOME_UTILIZADOR_ADMINISTRADOR_PADRAO);

            if (utilizador == null)
            {
                utilizador = new IdentityUser(NOME_UTILIZADOR_ADMINISTRADOR_PADRAO);
                await gestorUtilizadores.CreateAsync(utilizador, PASSWORD_UTILIZADOR_ADMIN_PADRAO);
            }

        }
    }
}
