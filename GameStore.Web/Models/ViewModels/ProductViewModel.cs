using System.ComponentModel.DataAnnotations;

namespace GameStore.Web.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Condition { get; set; }
        [Required]
        public int Discount { get; set; }
    }
}