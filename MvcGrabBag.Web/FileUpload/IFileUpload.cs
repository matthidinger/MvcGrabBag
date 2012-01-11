using System.IO;

namespace MvcGrabBag.Web.FileUpload
{
    public interface IFileUpload
    {
        int ContentLength { get; }
        string ContentType { get; }
        string FileName { get;  }
        Stream InputStream { get; }
    }
}