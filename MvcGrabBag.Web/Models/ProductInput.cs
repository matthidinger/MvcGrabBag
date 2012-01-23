using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcGrabBag.Web.FileUpload;
using MvcGrabBag.Web.Selectors;

namespace MvcGrabBag.Web.Models
{
    public class ProductInput
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Description = "A brief description of the product")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [Display(Description = "An optional alternate description of the product to display when featured on the home page")]
        [DataType(DataType.MultilineText)]
        public string FeaturedDescription { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Display(Description = "Price of the product during a sale")]
        public decimal? SalePrice { get; set; }


        [Required]
        public UploadedFile Thumbnail { get; set; }


        [Required]
        [CategorySelector]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }


    }
}