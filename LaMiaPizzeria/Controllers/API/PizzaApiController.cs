using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.ModelForViews;
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
                    return NotFound();
                }
            }
        }

        [HttpGet("{name}")]
        public IActionResult SearchByName(string name)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToFind = db.Pizza.Where(p => p.Name.Contains(name)).Include(p => p.Category).FirstOrDefault();
                if (pizzaToFind != null)
                {
                    return Ok(pizzaToFind);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pizza pizzaToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    db.Pizza.Add(pizzaToAdd);
                    db.SaveChanges();

                    return Ok();
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Modify([FromBody] Pizza modifiedPizza, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                using (PizzaShopContext db = new PizzaShopContext())
                {
                    Pizza? pizzaToModify = db.Pizza.Where(p => p.Id == id).FirstOrDefault();
                    if (pizzaToModify != null)
                    {
                        pizzaToModify.Name = modifiedPizza.Name;
                        pizzaToModify.Description = modifiedPizza.Description;
                        pizzaToModify.Price = modifiedPizza.Price;
                        pizzaToModify.ImageUrl = modifiedPizza.ImageUrl;
                        pizzaToModify.CategoryId = modifiedPizza.CategoryId;

                        db.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (PizzaShopContext db = new PizzaShopContext())
            {
                Pizza? pizzaToDelete = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return Ok();
                }
                else
                {
                    return NotFound();

                }
            }
        }
    }
}
