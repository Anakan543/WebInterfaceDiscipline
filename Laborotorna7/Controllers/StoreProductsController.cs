using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Laborotorna7.Service;

namespace Laborotorna7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreProductsController : ControllerBase
    {
        private readonly IStoreProdictService _IStoreProdictService;

        public StoreProductsController (IStoreProdictService iStoreProdictService)
        {
            _IStoreProdictService = iStoreProdictService;
        }

        [HttpGet(Name = "GetItemsAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _IStoreProdictService.GetStoreProducts());
        }
        [HttpGet("name",Name = "GetItemsWithModel")]
        public async Task<IActionResult> GetItemsWithModel(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return BadRequest("Empty param model");
            }

            var allProducts = await _IStoreProdictService.GetStoreProducts();

            var matchingProducts = allProducts.Where(item => item.Model == model.Trim()).ToList();

            if (matchingProducts.Any())
            {
                return Ok(matchingProducts);
            }
            return NotFound("No products found ");
        }

        [HttpPost(Name = "InsertItems")]
        public async Task<IActionResult> InsertItems(StoreProducts products)
        {
            if (products == null) { return BadRequest("Null param"); }

            var createdProduct = await _IStoreProdictService.CreateProducts(products);
            return Ok(createdProduct);
        }

        [HttpPut(Name = "UpdateItems")]

        public async Task<IActionResult> UpdateItems(string model, string manufacture, StoreProducts products)
        {
            var updatedProduct = await _IStoreProdictService.UpdateProducts(model, manufacture, products);
            if (updatedProduct == null)
            {
                return NotFound("Product not found");
            }
            return Ok(updatedProduct);
        }

        [HttpDelete(Name = "DeleteItems")]

        public async Task<IActionResult> DeleteItems(string model, string manufacture)
        {
            var result = await _IStoreProdictService.DeleteProducts(model, manufacture);
            if (!result)
            {
                return NotFound("Product not found");
            }
            return Ok("Product deleted successfully");
        }
    }
}
