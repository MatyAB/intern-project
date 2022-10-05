using DemoWebCam.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebCam.Controllers
{
    public class CamraController : Controller
    {
        private readonly IWebHostEnvironment _environment;
      

        public CamraController(IWebHostEnvironment hostingEnvironment)
        {
            this._environment = hostingEnvironment;
        }
        faceresult fc = new faceresult();
        [HttpGet]
        public IActionResult Capture()
        {
            fc.Result = true;

            ViewBag.Result = fc.Result;
            return View();
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {

            try
            {
                var files = HttpContext.Request.Form.Files;

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var myUniqueFileName = string.Format(@"{0}.jpg", DateTime.Now.Ticks);
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filePath = Path.Combine(_environment.WebRootPath, "blackList") + $@"\{newFileName}";

                            if (!string.IsNullOrEmpty(filePath))
                            {
                                // delete Image if face results are match else store in camera photos folder
                                if(fc.Result==true)
                                {
                                    FileInfo fi = new FileInfo(fileName);
                                    System.IO.File.Delete(fileName);
                                    if (fi.LastAccessTime < DateTime.Now.AddHours(-1)) fi.Delete();
                                }

                                StoreInFolder(file, filePath);
                                foreach (var f in files)
                                {
                                    FileInfo fi = new FileInfo(fileName);
                                    if (fi.LastAccessTime < DateTime.Now.AddHours(-1)) fi.Delete();
                                }




                            }
                            var imageBytes = System.IO.File.ReadAllBytes(filePath);
                            var FilePath = Path.Combine(_environment.WebRootPath, "CameraPhoto" + $@"\{newFileName}");
                            if (fc.Result == false)
                            {
                                //  Storing Image if face results are match 

                                StoreInFolder(file, FilePath);

                            }
                            foreach (var f in files)
                            {
                                FileInfo fi = new FileInfo(fileName);
                                if (fi.LastAccessTime < DateTime.Now.AddHours(-1)) fi.Delete();
                            }
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }

            }
            catch (Exception)
            {
                throw;
            }


        }
        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

        }
        private void DeteleImage(IFormFile file, string fileName)
        {
            if(fileName.LastWriteTime<=DateTime.Now.AddHours(-1))
            {

            }
        }
        private void StoreInDatabase(byte[] imageBytes)
        {
            //saving captured into database
        }

    }
}                      