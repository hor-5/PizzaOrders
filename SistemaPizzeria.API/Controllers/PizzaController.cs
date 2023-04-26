using Microsoft.AspNetCore.Mvc;
using SistemaPizzeria.Entidades;
using SistemaPizzeria.Servicios;

namespace SistemaPizzeria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        PizzaService pizzaServices = new PizzaService();

        // GET all action
        [HttpGet]
        [Route("/pizzas/all")]
        public ActionResult<List<Pizza>> GetAll() => pizzaServices.GetAllPizzas();

        // GET by Id action
        [HttpGet]
        [Route("/pizzas/{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = pizzaServices.GetPizzaById(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        // POST action
        [HttpPost]
        public ActionResult<Pizza> CreatePizza(Pizza pizza) {
            Pizza insertedPizza = pizzaServices.CreatePizza(pizza);
            return CreatedAtAction(nameof(Get), new { id = insertedPizza.IdPizza }, insertedPizza);
        }
        

        // PUT action
        [HttpPut]
        [Route("/pizzas/{id}")]
        public ActionResult<Pizza> UpdatePizza(int id, Pizza pizza) {
           
            if(id!= pizza.IdPizza)
            {
                return BadRequest();
            }
            var existingPizza = pizzaServices.GetPizzaById(id);
            if(existingPizza is null)
            {
                return NotFound();
            }

           pizzaServices.UpdatePizza(pizza);
            Pizza updatedPizza = pizzaServices.GetPizzaById(id);
            return updatedPizza;
        } 
        // DELETE action

        [HttpDelete]
        [Route("/pizzas/{id}")]
        public ActionResult<int> DeletePizza(int id) {
            int affectedRows = pizzaServices.DeletePizza(id);
            return affectedRows == 1 ? Ok(new { AffectedRows = affectedRows, message = $"Pizza #{id} succesfully deleted." }) : NotFound();

        } 
    }
}
