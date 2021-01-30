using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginaPessoal_BD.Models
{
    public class ListaExperienciasVIewModel
    {
        
        public List<Experiencia> Experiencias { get; set; } //estou a passar para aqui a lista de experiencias

        public Paginacao Paginacao { get; set; }
    }
}
