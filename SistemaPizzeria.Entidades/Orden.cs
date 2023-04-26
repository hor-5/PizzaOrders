using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPizzeria.Entidades
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public string? Notes { get; set; }
        public bool IsComplete { get; set; }
        public List<PizzaEncargada> pizzasEncargadas { get; set; }
        public double Total { get; set; }

        //public Date CreatedAt{get;set;}

    }
}
