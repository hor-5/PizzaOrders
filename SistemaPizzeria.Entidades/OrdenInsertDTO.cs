using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPizzeria.Entidades
{
    public class OrdenInsertDTO
    {
       
        public string Notes { get; set; }
        //public bool IsCompleted { get; set; }
        public List<PizzaEncargadaDTO> pizzasEncargadas { get; set; }
        //public double Total { get; set; }
    }
}
