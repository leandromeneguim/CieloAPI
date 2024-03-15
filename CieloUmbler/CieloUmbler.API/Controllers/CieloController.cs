using CieloUmbler.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CieloUmbler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CieloController : ControllerBase
    {
        private readonly ILogger<CieloController> _logger;
        private readonly ITransation _transation;

        public CieloController(ILogger<CieloController> logger, ITransation transation)
        {
            _logger = logger;
            _transation = transation;
        }

        public async Task<Dictionary<string,object>> ConsultaBin(string cardBin)
        {
            return await _transation.QueryCardBin(cardBin);
        }
    }
}
