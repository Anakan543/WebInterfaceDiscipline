using laborotorna6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;

namespace laborotorna6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreProductsController : ControllerBase
    {
        private List<StoreProducts> productsList = new List<StoreProducts> {
            new StoreProducts { Model = "130/900 EQ2", Manufacture = "Arsenal", Price = 20.16, Type = "Reflector", Count = 7, Type_item = "telescops" }, 
            new StoreProducts { Model = "GSO 8 \"Dobson Deluxe\"", Manufacture = "Arsenal", Price = 47.38, Type = "Reflector", Count = 1, Type_item = "telescops"  },
            new StoreProducts { Model = "70AZ2", Manufacture = "Sky-Watcher", Price = 7.70, Type = "Refraktor", Count = 10, Type_item = "telescops" }, 
            new StoreProducts { Model = "10x50 Porro", Manufacture = "Sky-Watcher", Price = 3.36, Type = "Porro", Count = 2, Type_item = "binoculars" },
            new StoreProducts { Model = "Crossfire HD 12x50 WP", Manufacture = "Vortex", Price = 11.99, Type = "Roof", Count = 5, Type_item = "binoculars"  }, 
            new StoreProducts { Model = "Konusarmy 10x50 WA", Manufacture = "Konus", Price = 2.20, Type = "Porro", Count = 2, Type_item = "binoculars"  }
        };

        [HttpGet(Name = "GetItemsAll")]
        public IEnumerable<StoreProducts> Get()
        {
            return productsList;
        }
        [HttpGet("name",Name = "GetItemsForName")]

        public IActionResult GetItemsWithModel(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return BadRequest("Empty param model");
            }

            List<StoreProducts> matchingProducts = productsList.Where(item => item.Model == model.Trim()).ToList();
            if (matchingProducts.Any())
            {
                return Ok(matchingProducts);
            }
            return NotFound("No products found ");
        }

        [HttpPost(Name = "InsertItems")]
        public IActionResult InsertItems(StoreProducts products)
        {
            if (products == null || productsList.Any(p => p.Model == products.Model.Trim() && p.Manufacture == products.Manufacture.Trim())){
                return BadRequest("products with param already exists");
            }
            productsList.Add(products);
            return Ok(products);
        }
    }
}
