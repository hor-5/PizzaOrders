namespace SistemaPizzeria.Entidades
{
    public class PizzaEncargada 
    {
        public int IdPizza { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }
        public List<Topping>? toppingsSeleccionados { get; set; }
    }
}
