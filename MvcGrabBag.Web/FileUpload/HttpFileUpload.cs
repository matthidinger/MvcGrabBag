using System;
using System.IO;
using System.Web;

namespace MvcGrabBag.Web.FileUpload
{
    public class HttpFileUpload : IFileUpload
    {
        private readonly HttpPostedFileBase _postedFile;

        public HttpFileUpload(HttpPostedFileBase postedFile)
        {
            if (postedFile == null) throw new ArgumentNullException("postedFile");
            _postedFile = postedFile;
        }

        public int ContentLength
        {
            get { return _postedFile.ContentLength; }
        }

        public string ContentType
        {
            get { return _postedFile.ContentType; }
        }

        public string FileName
        {
            get { return _postedFile.FileName; }
        }

        public Stream InputStream
        {
            get { return _postedFile.InputStream; }
        }
    }
}