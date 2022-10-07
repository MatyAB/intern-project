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
        public bool facematch = true;

        public CamraController(IWebHostEnvironment hostingEnvironment)
        {
            this._environment = hostingEnvironment;
        }
        faceresult fc = new faceresult();
        private int i;


        [HttpGet]
        public IActionResult Capture()
        {
            fc.Result = facematch;

            ViewBag.Result = fc.Result;
            return View();
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {


            var files = HttpContext.Request.Form.Files;


            if (files != null)
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)


                    {

                        var fileName = file.FileName;

                        var fileExtension = Path.GetExtension(fileName);
                        var CaptureFileName = string.Concat((string?)string.Format("vvv"), fileExtension);
                        var FilePath = Path.Combine(_environment.WebRootPath, "CameraPhoto" + $@"\{CaptureFileName}");


                        if (!string.IsNullOrEmpty(FilePath))
                        {

                            StoreInFolder(file, FilePath);
                            //FileInfo fi = new FileInfo(FilePath);
                            // if (fi.LastAccessTime < DateTime.Now.AddHours(-1)) fi.Delete();

                        }



                        var fileName1 = file.FileName;
                        var myUniqueFileName = string.Format(@"{0}.jpg", DateTime.Now.Ticks);
                        var fileExtension1 = Path.GetExtension(fileName1);
                        var newFileName1 = string.Concat(myUniqueFileName, fileExtension1);
                        var filePath = Path.Combine(_environment.WebRootPath, "blackList") + $@"\{newFileName1}";

                        //var fileName1 = file.FileName;
                        //    var fileExtension1 = Path.GetExtension(fileName1);
                        //    var newFileName1 = string.Concat(string.Format("pic") + i++, fileExtension1);
                        //    var filePath = Path.Combine(_environment.WebRootPath, "blackList") + $@"\{newFileName1}";

                        if (facematch == false)
                        {

                            StoreInFolderBlackList(file, filePath);
                        }




                        //Detete file from folder
                        if (System.IO.File.Exists(@"C:\Users\Hp\Desktop\projectfronm cha\WebAppl\wwwroot\CameraPhoto\638005724705688983.jpg"))
                        {
                            // Use a try block to catch IOExceptions, to
                            // handle the case of the file already being
                            // opened by another process.
                            try
                            {
                                System.IO.File.Delete(@"C:\Users\Hp\Desktop\projectfronm cha\WebAppl\wwwroot\CameraPhoto\638005724705688983.jpg");
                            }
                            catch (System.IO.IOException e)
                            {
                                Console.WriteLine(e.Message);

                            }

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


        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

        }
        private void StoreInFolderBlackList(IFormFile file, string fileName1)
        {
            using (FileStream fs = System.IO.File.Create(fileName1))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

        }






    }
}