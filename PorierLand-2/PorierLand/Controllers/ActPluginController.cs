using FFXIVTranslator.PorierFFXIV;
using Microsoft.AspNetCore.Mvc;

namespace PorierLand.Controllers
{
    [Route("api/act_plugin")]
    public class ActPluginController : Controller
    {
        private readonly PorierFFXIVDbContext _context;

        public ActPluginController(PorierFFXIVDbContext context)
        {
            _context = context;
        }

        [HttpGet("version")]
        public string GetVersion()
        {
            return "1.2";
        }
        
        [HttpGet("latest")]
        public string GetLatest()
        {
            return "https://github.com/porier/PorierACTPlugin/releases/download/1.2/PorierACTPlugin.dll";
        }

        [HttpGet("latest_portable")]
        public string GetLatestPortable()
        {
            return "https://github.com/porier/PorierACTPlugin/releases/download/1.2/PorierACTPlugin-portable.dll";
        }
    }
}