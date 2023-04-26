using SistemaPizzeria.Entidades;
using SistemaPizzeria.Datastore;

namespace SistemaPizzeria.Servicios
{
    public class PizzaService
    {
       DataStore dataLayer = new DataStore();
        public List<Pizza> GetAllPizzas() => dataLayer.getPizzas();
        public Pizza GetPizzaById(int id) => dataLayer.getPizzaById(id);
        public Pizza CreatePizza(Pizza pizza) {
            int idInserted = dataLayer.insertPizza(pizza);
            Pizza pizzaInserted = dataLayer.getPizzaById(idInserted);
            return pizzaInserted;
        }
        public int UpdatePizza(Pizza pizza) => dataLayer.updatePizza(pizza);
        public int DeletePizza(int id) => dataLayer.deletePizza(id);
    }
}
