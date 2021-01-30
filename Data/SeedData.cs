using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Data
{
    public class SeedData
    {

        internal static void PreencheDadosFicticiosExperiencia(PaginaPessoalBDContext context)
        {
            InsereExperienciasFicticias(context);
        }

        private static void InsereExperienciasFicticias(PaginaPessoalBDContext context)
        {
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
                }
            });

            context.SaveChanges();

        }
    }
}
