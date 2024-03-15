using CieloUmbler.API.Domain.Entities;
using CieloUmbler.API.Infrastructure.Data;
using CieloUmbler.API.Services;
using CieloUmbler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly ITransation _transation;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _transation = new CieloTransation(_config);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Save(IFormCollection card, decimal valor)
        {
            var binCard = card["CardNumber"][0].Replace(" ","").Substring(0,6);
            var retorno = await Task.Run(() => _transation.QueryCardBin(binCard));
            if (retorno.ContainsKey("Code"))
            {
                ViewBag.messageError = retorno["Message"];
                return View("Index", card);
            }
            Card dadosColetados = new(customer: "Teste",
                                      holder: "Teste",
                                      cardNumber: card["CardNumber"][0].Replace(" ", ""),
                                      expirationDate: card["ExpirationDate"][0],
                                      securityCode: card["SecurityCode"][0],
                                      brand: card["Brand"][0]);
            retorno = await Task.Run(() => _transation.TokenCard(dadosColetados));
            var cardToken = retorno["CardToken"];

            Payment payment = new Payment(TypeOfPayment.CreditCard.ToString(),
                                          (decimal)valor,
                                          1,
                                          dadosColetados);

            var capture = new Capture("12345678",payment, new Customer("Teste", null));

            retorno = await _transation.AuthenticateCard(capture);
            if (retorno.ContainsKey("Code"))
            {
                ViewBag.messageError = retorno["Message"];
                return View("Index");
            }
            ViewBag.retorno = retorno["PaymentId"];
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
