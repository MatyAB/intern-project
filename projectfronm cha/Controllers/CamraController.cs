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
    public class CamraControllerr : Controller
    {
        private readonly IWebHostEnvironment _environment;
       
        public bool facematch = false;


        public CamraControllerr(IWebHostEnvironment hostingEnvironment)
        {
            this._environment = hostingEnvironment;
        }
        faceresult fc = new faceresult();
        [HttpGet]
        public IActionResult Capture()
        {
            fc.Result = false;
            ViewBag.Result = fc.Result;
            return View();
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {

            try
            {
                var files = HttpContext.Request.Form.Files;
               
                if (files != null )
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0 )
                        {
                            var fileName = file.FileName;
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filePath = Path.Combine(_environment.WebRootPath, "blackList") + $@"\{newFileName}";

                            if (!string.IsNullOrEmpty(filePath))
                            {
                                // Storing Image in Folder  
                                StoreInFolder(file, filePath);
                                

                            }
                            var imageBytes = System.IO.File.ReadAllBytes(filePath);
                            var FilePath = Path.Combine(_environment.WebRootPath, "CameraPhoto" + $@"\{newFileName}");
                            if (facematch!=true)
                            {
                                //  StoreInDataBase(imageBytes);
                                
                               StoreInFolder(file, FilePath);
                                System.IO.File.Delete(filePath);
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
        //private void StoreInDatabase(byte[] imageBytes)
        //{
        //    //saving captured into database
        //}

    }
}                      