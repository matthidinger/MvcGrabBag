using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcGrabBag.Web.Models
{
    public class ProductInput
    {
        public ProductInput()
        {
            DisplayModeReadOnly = new List<ProductDisplayMode> { ProductDisplayMode.HomePage, ProductDisplayMode.BrowseOnly};
        }
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


        [Required]
        [ProductDisplayModeSelector(MaxRadioButtons = 0)]
        public ProductDisplayMode? DisplayMode { get; set; }

        [Required]
        [ProductDisplayModeSelector]
        public ProductDisplayMode? DisplayModeRadio { get; set; }


        [Required]
        [ProductDisplayModeSelector(MaxRadioButtons = 0)]
        public List<ProductDisplayMode> DisplayModesListBox { get; set; }


        [Required]
        [ProductDisplayModeSelector]
        public List<ProductDisplayMode> DisplayModes { get; set; }

        [ProductDisplayModeSelector]
        [ReadOnly(true)]
        public List<ProductDisplayMode> DisplayModeReadOnly { get; set; }
    }

    public class ProductDisplayModeSelectorAttribute : SelectorAttribute
    {
        public override IEnumerable<SelectListItem> GetOptions()
        {
            return Selector.GetItemsFromEnum<ProductDisplayMode>();
        }
    }


    public enum ProductDisplayMode
    {
        [Display(Description = "Only show on home page")]
        HomePage,
        [Display(Description = "Only show on search results")]
        SearchResults,
        [Display(Description = "Only allow browsing")]
        BrowseOnly
    }

}