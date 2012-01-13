using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace MvcGrabBag.Web.FileUpload
{
    [ModelBinder(typeof(UploadedFileModelBinder))]
    public class UploadedFile
    {
        public UploadedFile()
        {
            
        }

        public UploadedFile(HttpPostedFileBase postedFile)
        {
            ContentType = postedFile.ContentType;
            ContentLength = postedFile.ContentLength;
            FileName = postedFile.FileName;
            OpenStream = () => postedFile.InputStream;
        }

        public UploadedFile(string fileName)
        {
            var file = new FileInfo(fileName);
            ContentLength = (int) file.Length;
            FileName = file.Name;
            OpenStream = () => file.Open(FileMode.OpenOrCreate);
        }


        public UploadedFile NewFile { get; set; }

        public int ContentLength { get; private set; }
        public string ContentType { get; private set; }
        public string FileName { get; private set; }
        public Func<Stream> OpenStream { get; private set; } 

        public bool HasNewFile
        {
            get { return NewFile != null && NewFile.ContentLength > 0; }
        }
    }
}