using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcGoodies.Web.Models
{
    public class ProductInput
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Description = "A brief description of the product")]
        [MultiLine]
        public string Description { get; set; }


        [Display(Description = "An optional alternate description of the product to display when featured on the home page")]
        [MultiLine]
        public string FeaturedDescription { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Display(Description = "Price of the product during a sale")]
        public decimal? SalePrice { get; set; }

    }
}