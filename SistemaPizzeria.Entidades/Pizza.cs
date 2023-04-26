namespace SistemaPizzeria.Entidades
{
    public class Pizza
    {
        public int IdPizza { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }        

    }
}
