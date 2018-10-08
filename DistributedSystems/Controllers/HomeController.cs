using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DistribuedSystem.Common;
using DistribuedSystem.Common.Interfaces;
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

        public async Task<IActionResult> Register(OrderViewModel model)
        {
            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{RabbitMqConstants.RabbitMqUri}" +
                                    $"{RabbitMqConstants.RegisterOrderServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);

            await endPoint.Send<IRegisterOrderCommand>(new
            {
                PickupName = model.PickupName,
                PickupAddress = model.PickupAddress,
                PickupCity = model.PickupCity,
                DeliverName = model.DeliverName,
                DeliverAddress = model.DeliverAddress,
                DeliverCity = model.DeliverCity,
                Weight = model.Weight,
                Fragile = model.Fragile,
                Oversized = model.Oversized
            });

            return View("Thanks");
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
