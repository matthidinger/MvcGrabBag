using System.ComponentModel.DataAnnotations;
using System.IO;
using MvcGrabBag.Web.FileUpload;

namespace MvcGrabBag.Web.Models
{
    public class ProductThumbnail
    {
        public ProductThumbnail(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                Thumbnail = new UploadedFile(Path.Combine(Path.GetTempPath(), fileName));
            }
        }

        [Required]
        public UploadedFile Thumbnail { get; set; }
    }
}