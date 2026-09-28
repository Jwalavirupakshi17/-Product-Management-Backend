using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.DTOs;
using ProductManagement.Data.Models;
using ProductManagement.Services.Interfaces;

namespace ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var dtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            });
            return Ok(dtos); // Returns 200 OK[cite: 1]
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound(); // Returns 404 if unknown[cite: 1]

            return Ok(new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = new Product
            {
                ProductName = dto.ProductName,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            var createdProduct = await _productService.CreateProductAsync(product);

            var returnDto = new ProductDto
            {
                ProductId = createdProduct.ProductId,
                ProductName = createdProduct.ProductName,
                Category = createdProduct.Category,
                Price = createdProduct.Price,
                StockQuantity = createdProduct.StockQuantity
            };

            return CreatedAtAction(nameof(GetProduct), new { id = returnDto.ProductId }, returnDto); // Returns 201 Created[cite: 1]
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = new Product
            {
                ProductId = id,
                ProductName = dto.ProductName,
                Category = dto.Category,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            var success = await _productService.UpdateProductAsync(product);
            if (!success) return NotFound(); // Returns 404 if missing[cite: 1]

            return NoContent(); // Returns 204 No Content[cite: 1]
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success) return NotFound(); // Returns 404 if missing[cite: 1]

            return NoContent(); // Returns 204 No Content[cite: 1]
        }
    }
}