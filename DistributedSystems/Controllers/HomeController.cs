using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DistributedSystems.Models;
using DistributedSystems.Web;
using DistributedSystems.Web.Models;

namespace DistributedSystems.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Register(OrderViewModel model)
        {
            var command = new RegisterOrderCommand(model);

            using (var rabbitMqManager = new RabbitMqManager())
            {
                rabbitMqManager.SendRegisterOrderCommand(command);
            }
            return RedirectToAction("Thanks");
        }

        public IActionResult Thanks()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
