using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Models
{
    public class Paginacao
    {
        public const int NUMERO_ITEMS_PAGINA_PADRAO = 2;
        public const int NUMERO_PAGINAS_MOSTRAR_ANTES_DEPOIS = 1;
        public int PaginaAtual { get; set; }

        public int ItemsPorPagina { get; set; } = NUMERO_ITEMS_PAGINA_PADRAO;

        public int TotalItems { get; set; }

        public int TotalPaginas => (int)Math.Ceiling((double)TotalItems / ItemsPorPagina);
    }
}
