using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Data
{
    public class SeedData
    {
        private const string NOME_UTILIZADOR_ADMINISTRADOR_PADRAO = "admin@lhdb.pt";
        private const string PASSWORD_UTILIZADOR_ADMIN_PADRAO = "Qwerty_1";
        private const string ROLE_ADMIN = "admin";
        private const string ROLE_USUARIOS = "usuarios";
        private const string ROLE_VISITANTES = "visitantes";

        internal static void PreencheDadosFicticiosExperiencia(PaginaPessoalBDContext context)
        {
            InsereExperienciasFicticias(context);
        }

        private static void InsereExperienciasFicticias(PaginaPessoalBDContext context)
        {
            //InsereExperienciasFicticiasParaTestes(context);

            if (context.Experiencia.Any()) return;

            context.Experiencia.AddRange(new Models.Experiencia[]
            {
                new Models.Experiencia
                {
                    NomeEmpresa = "XPTO Serviços, Lda",
                    Cargo = "Gestor",
                    //DataInicio =

                    //DataFim =
                    Funcoes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },

                new Models.Experiencia
                {
                    NomeEmpresa = "MKT Marques, SA",
                    Cargo = "Marketeer",
                    //DataInicio = 
                    //DataFim =
                    Funcoes = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."
                },

                new Models.Experiencia
                {
                    NomeEmpresa = "Lorium, SA",
                    Cargo = "Marketeer",
                    //DataInicio = 
                    //DataFim =
                    Funcoes = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."
                },

                new Models.Experiencia
                {
                    NomeEmpresa = "Lalala, SA",
                    Cargo = "Manager",
                    //DataInicio = 
                    //DataFim =
                    Funcoes = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."
                },

                new Models.Experiencia
                {
                    NomeEmpresa = "Lalali, SA",
                    Cargo = "Manager",
                    //DataInicio = 
                    //DataFim =
                    Funcoes = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo."
                }
            });

            context.SaveChanges();

        }

        internal static async Task InsereRolesAsync(RoleManager<IdentityRole> gestorRoles)
        {
            //acrescento aqui quando precisar de criar mais Roles
            await CriaRoleSeNaoExiste(gestorRoles, ROLE_ADMIN); //administrador do site
            await CriaRoleSeNaoExiste(gestorRoles, ROLE_USUARIOS); //com poderes para CRUD
            await CriaRoleSeNaoExiste(gestorRoles, ROLE_VISITANTES); //visitantes do site

        }

        internal static async Task InsereUsuariosFicticiosAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            IdentityUser visitante = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, "miguel@hotmail.pt", "Qwerty_1");
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, visitante, ROLE_VISITANTES);
        }

        private static async Task CriaRoleSeNaoExiste(RoleManager<IdentityRole> gestorRoles, string funcao)
        {
            if (!await gestorRoles.RoleExistsAsync(funcao))
            {
                IdentityRole role = new IdentityRole(funcao);
                await gestorRoles.CreateAsync(role);
            }
        }

        private static void InsereExperienciasFicticiasParaTestes(PaginaPessoalBDContext context)
        {
            for (int i = 0; i < 4; i++)
            {
                context.Experiencia.Add(new Models.Experiencia
                {
                    NomeEmpresa = "Empresa " + i,
                    Cargo = "Gestor" + i,
                    DataInicio = default,
                    DataFim = default,
                    Funcoes = "Monitorizar" + i
                });
            }

            context.SaveChanges();
        }

        internal static async Task InsereAdministradorPadraoAsync(UserManager<IdentityUser> gestorUtilizadores)
        {
            string nomesUtilizador = NOME_UTILIZADOR_ADMINISTRADOR_PADRAO;
            string passwordUtilizador = PASSWORD_UTILIZADOR_ADMIN_PADRAO;
            IdentityUser utilizador = await CriaUtilizadorSeNaoExiste(gestorUtilizadores, nomesUtilizador, passwordUtilizador);

            
            await AdicionaUtilizadorRoleSeNecessario(gestorUtilizadores, utilizador, ROLE_ADMIN);

        }

        private static async Task AdicionaUtilizadorRoleSeNecessario(UserManager<IdentityUser> gestorUtilizadores, IdentityUser utilizador, string role)
        {
            if (!await gestorUtilizadores.IsInRoleAsync(utilizador, role))
            {
                await gestorUtilizadores.AddToRoleAsync(utilizador, role);
            }
        }

        private static async Task<IdentityUser> CriaUtilizadorSeNaoExiste(UserManager<IdentityUser> gestorUtilizadores, string nomesUtilizador, string passwordUtilizador)
        {
            IdentityUser utilizador = await gestorUtilizadores.FindByNameAsync(nomesUtilizador);

            if (utilizador == null)
            {
                utilizador = new IdentityUser(nomesUtilizador);

                await gestorUtilizadores.CreateAsync(utilizador, passwordUtilizador);
            }

            return utilizador;
        }
    }
}
