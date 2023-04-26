using SistemaPizzeria.Entidades;
using SistemaPizzeria.Datastore;

namespace SistemaPizzeria.Servicios
{
    public class ToppingService
    {
        DataStore dataLayer = new DataStore();
        public List<Topping> GetToppings() => dataLayer.getToppings();

        public Topping GetToppingById(int id) => dataLayer.getToppingById(id);

        public Topping CreateTopping(Topping topping)
        {
            int idInserted = dataLayer.insertTopping(topping);
            Topping toppingInserted = dataLayer.getToppingById(idInserted);
            return toppingInserted;
        }

        public int UpdateTopping(Topping topping) => dataLayer.updateTopping(topping);

        public int DeleteTopping(int id) => dataLayer.deleteTopping(id);

    }
}
