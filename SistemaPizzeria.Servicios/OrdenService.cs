using SistemaPizzeria.Entidades;
using SistemaPizzeria.Datastore;

namespace SistemaPizzeria.Servicios
{
    public class OrdenService
    {
        DataStore dataLayer = new DataStore();

        public List<Orden> GetAll() => dataLayer.getAllOrders();
        public Orden GetOrdenById(int id)=> dataLayer.getOrdenById(id);

        public Orden InsertOrden(OrdenInsertDTO orden) => dataLayer.insertOrden(orden);
    }
}
