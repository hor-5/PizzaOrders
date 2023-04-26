using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaPizzeria.Entidades
{
    public class Topping
    {
        public int IdTopping { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }        
        public double Price { get; set; }
    }
}
