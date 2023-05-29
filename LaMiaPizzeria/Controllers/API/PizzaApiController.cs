using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaAPIController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPizzas()
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                List<Pizza> pizzas = db.Pizza.ToList();
                return Ok(pizzas);
            }
        }

        [HttpGet("{id}")]
        public IActionResult SearchById(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToFind = db.Pizza.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
                if (pizzaToFind != null)
                {
                    return Ok(pizzaToFind);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet("{name}")]
        public IActionResult SearchByName(string name)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToFind = db.Pizza.Where(p => p.Name.Contains(name)).FirstOrDefault();
                if(pizzaToFind != null)
                {
                    return Ok(pizzaToFind);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    db.Pizza.Add(pizza);
                    db.SaveChanges();

                    return Ok();
                }
            }
        }
    }
}
