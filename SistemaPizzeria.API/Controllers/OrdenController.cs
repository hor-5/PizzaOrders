using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPizzeria.Entidades;
using SistemaPizzeria.Servicios;

namespace SistemaPizzeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        OrdenService ordenService = new OrdenService();

        // GET all action
        [HttpGet]
        [Route("/ordenes/all")]
        public ActionResult<List<Orden>> GetAll() => ordenService.GetAll();

        // GET by Id action
        [HttpGet]
        [Route("/ordenes/{id}")]
        public ActionResult<Orden> Get(int id)
        {
            var orden = ordenService.GetOrdenById(id);

            if (orden == null)
                return NotFound();

            return orden;
        }

        // POST action
        [HttpPost]
        public ActionResult<Orden> CreatePizza(OrdenInsertDTO orden)
        {
            Orden insertedOrden = ordenService.InsertOrden(orden);
            return CreatedAtAction(nameof(Get), new { id = insertedOrden.IdOrden }, insertedOrden);
        }
    }
}
