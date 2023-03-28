using Microsoft.AspNetCore.Mvc;
using CoffeeShop.Models;
using CoffeeShopApi.Interfaces;

namespace CoffeeShop.Controllers
{
    //defines a controller for the Coffee 
    [Route("api/[controller]")]
    [ApiController]

    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeRepository _coffeeRepository;

        // constructor injection of the Coffee repository
        public CoffeeController(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        //this gets the Coffee from the Coffee repository
        // https://localhost:5001/api/Coffee/
        [HttpGet]
        public IActionResult GetAllCoffee()
        {
            return Ok(_coffeeRepository.GetAllCoffee());
        }

        //gets a Coffee by its id from the repository
        // https://localhost:5001/api/Coffee/5
        [HttpGet("{id}")]
        public IActionResult GetCoffeeById(int id)
        {
            var coffee = _coffeeRepository.GetCoffeeById(id);
            if (coffee == null)
            {
                return NotFound();
            }
            return Ok(coffee);
        }

        //https://localhost:5001/api/Coffee/
        [HttpPost]

        //adds a new Coffee to the repository
        public IActionResult Post(Coffee coffee)
        {
            _coffeeRepository.AddCoffee(coffee.Title, coffee.BeanVarietyId);
            return CreatedAtAction("GetCoffeeById", new { id = coffee.Id }, coffee);
        }

        //updates an existing Coffee in the repository by its id
        // https://localhost:5001/api/Coffee/5
        [HttpPut("{id}")]

        public IActionResult Put(int id, Coffee coffee)
        {
            var thisUpdatedCoffee = _coffeeRepository.GetCoffeeById(coffee.Id);
            if (thisUpdatedCoffee == null)
            {
                return NotFound();
            }

            _coffeeRepository.UpdateCoffee(id, coffee);
            return NoContent();
        }

        //delets a Coffee from the repository by its id
        // https://localhost:5001/api/Coffee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _coffeeRepository.Delete(id);
            return NoContent();
        }
    }
}