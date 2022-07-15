using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace StudentCityUI.Controllers.Sharing
{
    public class PhotoUploadService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        private string RootPath;
        private string profilePhotosPath;

        private string[] ImageExtentions = new string[] { "gif", "jpg", "png", "jpeg", "PNG" };

        public PhotoUploadService(IHostingEnvironment hostingEnvironment, string rootPath)
        {
            this.hostingEnvironment = hostingEnvironment;
            string contentRoot = this.hostingEnvironment.WebRootPath;
            profilePhotosPath = Path.Combine(contentRoot, "images");
            this.RootPath = rootPath;
        }
        public string UploadPhotoFromRequest(IFormFile Photo) {
            var extention = Photo.FileName.Substring(Photo.FileName.LastIndexOf('.') + 1);
            if (ImageExtentions.Contains(extention))
            {
                var date = DateTime.Now;
                string returnFilePath = null;
                if (Photo.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    if (Photo.Length > 0)
                    {

                        string uniqueID = String.Format(
                   "{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                   date.Year, date.Month, date.Day,
                   date.Hour, date.Minute, date.Second, date.Millisecond
                   );
                        string contentRoot = this.hostingEnvironment.WebRootPath;
                        var fileSavePath = Path.Combine(contentRoot + "/Images/", uniqueID + "." + Photo.FileName.Split('.').LastOrDefault());
                        returnFilePath = "/Images/" + uniqueID + "." + Photo.FileName.Split('.').LastOrDefault();
                        using (var stream = new FileStream(fileSavePath, FileMode.Create))
                        {
                            Photo.CopyTo(stream);
                        }
                    }
                }
                return returnFilePath;
            }
            return null;
        }
        /***********************Images with jpg extention**************************/
        public List<string> UploadPhotosFromRequest(List<IFormFile> Photos)
        {
            List<string> Images = new List<string>();
            foreach (var Photo in Photos)
            {
                var extention = Photo.FileName.Substring(Photo.FileName.LastIndexOf('.') + 1);
                if (extention.Equals("jpg"))
                {
                    var date = DateTime.Now;
                    string returnFilePath = null;
                    if (Photo.Length > 0)
                    {
                        var filePath = Path.GetTempFileName();
                        if (Photo.Length > 0)
                        {

                            string uniqueID = String.Format(
                       "{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                       date.Year, date.Month, date.Day,
                       date.Hour, date.Minute, date.Second, date.Millisecond
                       );
                            string contentRoot = this.hostingEnvironment.WebRootPath;
                            var fileSavePath = Path.Combine(contentRoot + "/Images/", uniqueID + "." + Photo.FileName.Split('.').LastOrDefault());
                            returnFilePath = "/Images/" + uniqueID + "." + Photo.FileName.Split('.').LastOrDefault();
                            using (var stream = new FileStream(fileSavePath, FileMode.Create))
                            {
                                Photo.CopyTo(stream);
                            }
                        }
                    }
                    Images.Add(returnFilePath);
                }
            }
            return Images;
        }

        public string UploadPhoto(string file)
        {
           
            string[] fileData = file.Split(";base64,");
            var filename = Guid.NewGuid().ToString() + fileData[0].Split("/")[1];
            var filePath = Path.Combine(RootPath, filename);
            var filePathWithoutExtention = Path.Combine(RootPath, Path.GetFileNameWithoutExtension(filename));

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);
            using (var fs = new FileStream(filePath, FileMode.CreateNew))
            {
                var bytess = Convert.FromBase64String(fileData[1]);
                fs.Write(bytess, 0, bytess.Length);
                fs.Flush();
                fs.Close();
            }

            //HandleImage(filePath, filePathWithoutExtention);
            return filename;
        }

        //private void HandleImage(string filePath, string pathWithOutExtention)
        //{
        //    var originalImage = Image.FromFile(filePath);
        //    if (originalImage == null) throw new Exception("Error retrieving image for resizing");
        //    ImageCompressor.GetSmallImage(originalImage).Save(pathWithOutExtention + "_small.jpeg", ImageFormat.Jpeg);
        //    ImageCompressor.GetMediumImage(originalImage).Save(pathWithOutExtention + "_medium.jpeg", ImageFormat.Jpeg);
        //}

        //public string SaveFile(IFormFile file, string subFolderName)
        //{
        //    string fileExtentionPdf = null;
        //    string fileExtentionVideo = null;
        //    string filename;
        //    var fileExtention = Path.GetExtension(file.FileName)?.ToLower();
        //    if (fileExtention == ".docx" || fileExtention == ".doc") { fileExtentionPdf = ".pdf"; }
        //    if (fileExtention == ".wmv") { fileExtentionVideo = ".mp4"; }

        //    var newFileName = Guid.NewGuid().ToString();
        //    filename = newFileName + fileExtention;
        //    string directory, filePath, filePathWithOutExtention;
        //    if (ImageExtentions.Contains(fileExtention))
        //    {
        //        directory = Path.Combine(profilePhotosPath, subFolderName);
        //        filePath = Path.Combine(directory, filename);
        //        filePathWithOutExtention = Path.Combine(directory, newFileName);
        //    }
        //    else
        //    {
        //        directory = Path.Combine(RootPath, subFolderName);
        //        filePath = Path.Combine(directory, filename);
        //        filePathWithOutExtention = Path.Combine(directory, newFileName);
        //    }



        //    if (!Directory.Exists(directory))
        //        Directory.CreateDirectory(directory);
        //    try
        //    {
        //        using (FileStream fs = System.IO.File.Create(filePath))
        //        {
        //            file.CopyTo(fs);
        //            fs.Flush();


        //        }

        //        if (fileExtentionPdf != null)
        //        {

        //            SautinSoft.PdfMetamorphosis p = new SautinSoft.PdfMetamorphosis();


        //            if (p != null)
        //            {

        //                string pdfPath = Path.ChangeExtension(filePath, ".pdf");


        //                // 2. Convert DOCX file to PDF file 
        //                if (p.DocxToPdfConvertFile(filePath, pdfPath) == 0)
        //                {
        //                    // System.Diagnostics.Process.Start(pdfPath);
        //                    filename = newFileName + fileExtentionPdf;
        //                    if (System.IO.File.Exists(filePath))
        //                    {
        //                        System.IO.File.Delete(filePath);


        //                    }
        //                }
        //                else
        //                {
        //                    System.Console.WriteLine("Conversion failed!");
        //                    Console.ReadLine();
        //                }
        //            }
        //        }
        //        if (fileExtentionVideo != null)
        //        {


        //            var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
        //            var ffMpegs = new NReco.VideoConverter.OutputSettings();

        //            if (ffMpeg != null)
        //            {
        //                ffMpeg.ConvertMedia(filePath, filePathWithOutExtention + fileExtentionVideo, "mp4");
        //                //  string pdfPath = Path.ChangeExtension(filePath, ".pdf");



        //                filename = newFileName + fileExtentionVideo;
        //                if (System.IO.File.Exists(filePath))
        //                {
        //                    System.IO.File.Delete(filePath);


        //                }
        //            }


        //        }
        //        if (ImageExtentions.Contains(fileExtention))
        //        {
        //            HandleImage(filePath, filePathWithOutExtention);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }


        //    return "/" + subFolderName + "/" + filename;

        //}
    }
}