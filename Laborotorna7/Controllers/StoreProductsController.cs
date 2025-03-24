using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Laborotorna7.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;

namespace Laborotorna7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreProductsController : ControllerBase
    {
        private readonly IStoreProdictService _IStoreProdictService;

        public StoreProductsController(IStoreProdictService iStoreProdictService)
        {
            _IStoreProdictService = iStoreProdictService;
        }

        [HttpGet("GetAll",Name = "GetItemsAll")]
        [SwaggerOperation(
            Summary = "Отримаати всіх товари",
            Description = "Повернення всіх доступних товарів продуктів в магазині"
            )]
        [SwaggerResponse(200, "Список товарів був успішно повернений", typeof(List<StoreProducts>))]
        [SwaggerResponse(500, "Помилка сервера")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _IStoreProdictService.GetStoreProducts());
        }
        [HttpGet("GetItemsWithModel",Name = "GetItemsWithModel")]
        [SwaggerOperation(
            Summary = "Отримати товар за моделью",
            Description = "Повернення інформації про товар за вказаною моделью"
            )]
        [SwaggerResponse(200, "Продукти знайдено", typeof(List<StoreProducts>))]
        [SwaggerResponse(400, "Передано порожній параметр моделі")]
        [SwaggerResponse(404, "Продукт з указаною моделлю не знайдено")]
        [SwaggerResponse(500, "Помилка сервера")]
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

        [HttpPost("InsertItems",Name = "InsertItems")]
        [SwaggerOperation(
            Summary = "Додати новий продукт",
            Description = "Створює новий продукт у магазині на основі переданих даних"
        )]
        [SwaggerResponse(200, "Продукт успішно створено", typeof(StoreProducts))]
        [SwaggerResponse(400, "Передано некоректні дані")]
        [SwaggerResponse(500, "Помилка сервера")]
        public async Task<IActionResult> InsertItems(StoreProducts products)
        {
            if (products == null) { return BadRequest("Null param"); }

            var createdProduct = await _IStoreProdictService.CreateProducts(products);
            return Ok(createdProduct);
        }

        [HttpPut("UpdateItems",Name = "UpdateItems")]
        [SwaggerOperation(
            Summary = "Оновити продукт",
            Description = "Оновлює існуючий продукт за моделлю та виробником"
        )]
        [SwaggerResponse(200, "Продукт успішно оновлено", typeof(StoreProducts))]
        [SwaggerResponse(404, "Продукт не знайдено")]
        [SwaggerResponse(500, "Помилка сервера")]
        public async Task<IActionResult> UpdateItems(string model, string manufacture, StoreProducts products)
        {
            var updatedProduct = await _IStoreProdictService.UpdateProducts(model, manufacture, products);
            if (updatedProduct == null)
            {
                return NotFound("Product not found");
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("DeleteItems", Name = "DeleteItems")]
        [SwaggerOperation(
            Summary = "Видалити продукт",
            Description = "Видаляє продукт за указаною моделлю та виробником"
        )]
        [SwaggerResponse(200, "Продукт успішно видалено", typeof(string))]
        [SwaggerResponse(404, "Продукт не знайдено")]
        [SwaggerResponse(500, "Помилка сервера")]
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
