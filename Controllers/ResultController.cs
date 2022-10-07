using Microsoft.AspNetCore.Mvc;
using DemoWebCam.Models;
using System.IO;

namespace DemoWebCam.Controllers
{
    public class ResultController : Controller
    {
        bool facematch=false;
       
        public IActionResult Fail()
        {
            
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
