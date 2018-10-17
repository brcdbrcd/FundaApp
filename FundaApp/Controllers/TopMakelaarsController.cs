using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundaApp.Logger;
using FundaApp.Models.Out;
using FundaApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FundaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopMakelaarsController : ControllerBase
    {
        private readonly IFundaService service;
        private ILoggerManager logger;

        public TopMakelaarsController(IFundaService _service, ILoggerManager _logger)
        {
            service = _service;
            logger = _logger;
        }

        // GET api/topmakelaars
        [HttpGet]
        public async Task<ActionResult<Output>> GetAsync()
        {
            logger.LogInfo("Request received.");

            return await service.GetTopMakelaars(false);
        }

        // GET api/topmakelaars/tuin
        [HttpGet("tuin")]
        public async Task<ActionResult<Output>> GetAsyncWithTuin()
        {
            logger.LogInfo("Request received. (tuin)");

            return await service.GetTopMakelaars(true);
        }
    }
}
