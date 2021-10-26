using DataAccesLayer.Interfaces;
using DataAccesLayer.Models;
using LaboratoireXamarinAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoireXamarinAspCore.Controllers
{
    public class ForgotController : Controller
    {
        private IServicesNewLogin _service;
        public ForgotController(IServicesNewLogin service)
        {
            _service = service;
        }

        public IActionResult NewLogin(string token)
        {
            return View();
        }
        private static string P { get; set; }

        public IActionResult Test(string token)
         {
            FormPassword form = new FormPassword();
            P = token;
            return View(form);

        }
        [HttpPost]
        public IActionResult Test(FormPassword form)
        {
            try
            {
                form.Token = P;
                _service.NewPassword(new NewLoginModel
                {
                    NewPassword = form.Password,
                    Token = form.Token
                });
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                return RedirectToAction("NewLogin");
            }
            
        }
    }
}
