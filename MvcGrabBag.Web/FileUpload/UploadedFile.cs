using System;
using System.IO;
using System.Web.Mvc;

namespace MvcGrabBag.Web.FileUpload
{
    [ModelBinder(typeof(UploadedFileModelBinder))]
    public class UploadedFile
    {
        public UploadedFile()
        {

        }

        public UploadedFile(string uploadedPhysicalPath)
        {
            UploadedPhysicalPath = uploadedPhysicalPath;
        }

        public string UploadedPhysicalPath { get; set; }

        public Guid DocumentId { get; set; }

        public IFileUpload NewFile { get; set; }

        public string FileName
        {
            get { return Path.GetFileName(UploadedPhysicalPath); }
        }

        public bool HasNewFile
        {
            get { return NewFile != null && NewFile.ContentLength > 0; }
        }
    }
}