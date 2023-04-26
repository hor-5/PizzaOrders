using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaPizzeria.Entidades;
using SistemaPizzeria.Servicios;

namespace SistemaPizzeria.API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ToppingController : Controller
    {
        ToppingService toppingServices = new ToppingService();
        [HttpGet]
        [Route("/toppings/all")]
        public ActionResult<List<Topping>> GetAllToppings() => toppingServices.GetToppings();

        // GET by Id action
        [HttpGet]
        [Route("/toppings/{id}")]
        public ActionResult<Topping> Get(int id)
        {
            var topping = toppingServices.GetToppingById(id);

            if (topping == null)
                return NotFound();

            return topping;
        }

        // POST action
        [HttpPost]
        public ActionResult<Topping> CreatePizza(Topping topping)
        {
            Topping insertedTopping = toppingServices.CreateTopping(topping);
            return CreatedAtAction(nameof(Get), new { id = insertedTopping.IdTopping }, insertedTopping);
        }

        // PUT action
        [HttpPut]
        [Route("/toppings/{id}")]
        public ActionResult<Topping> UpdateTopping(int id, Topping topping)
        {

            if (id != topping.IdTopping)
            {
                return BadRequest();
            }
            var existingTopping = toppingServices.GetToppingById(id);
            if (existingTopping is null)
            {
                return NotFound();
            }

            toppingServices.UpdateTopping(topping);
            Topping updatedTopping = toppingServices.GetToppingById(id);
            return updatedTopping;
        }

        [HttpDelete]
        [Route("/toppings/{id}")]
        public ActionResult<int> DeleteTopping(int id)
        {
            int affectedRows = toppingServices.DeleteTopping(id);
            return affectedRows == 1 ? Ok(new { AffectedRows = affectedRows, message=$"Topping #{id} succesfully deleted." }) : NotFound();

        }

    }
}
