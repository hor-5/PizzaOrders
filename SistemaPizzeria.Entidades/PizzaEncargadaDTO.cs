using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPizzeria.Entidades
{
    public class PizzaEncargadaDTO
    {
        public int IdPizza { get; set; }
        public List<int>? IdToppingsSeleccionados { get; set; }
    }
}
