using System.ComponentModel.DataAnnotations;

namespace ProductManagement.API.DTOs
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Product name cannot be empty.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int StockQuantity { get; set; }
    }
}