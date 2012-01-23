using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MvcGrabBag.Web.Selectors;

namespace MvcGrabBag.Web.Models
{
    public class DisplayModeOptions
    {
        public DisplayModeOptions()
        {
            ReadOnly = new List<DisplayMode> { DisplayMode.HomePage, DisplayMode.BrowseOnly };
        }

        [Required]
        [DisplayModeSelector(BulkSelectionThreshold = 0)]
        public DisplayMode? DropDown { get; set; }

        [Required]
        [DisplayModeSelector]
        public DisplayMode? RadioButtons { get; set; }


        [Required]
        [DisplayModeSelector(BulkSelectionThreshold = 0)]
        public List<DisplayMode> ListBox { get; set; }

        [Required]
        [DisplayModeSelector]
        public List<DisplayMode> CheckBoxes { get; set; }

        [ReadOnly(true)]
        [DisplayModeSelector]
        public List<DisplayMode> ReadOnly { get; set; }

        [Required]
        [CategorySelector]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
    }
}