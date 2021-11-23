
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using SyZero;
using SyZero.AspNetCore.Controllers;
using SyZero.Configurations;
using SyZero.Serialization;

namespace Template1.Template2.Web.Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class instanceController : SyZeroController
    {
        private readonly IConfiguration _configuration;
        public IJsonSerialize jsonSerialize { get; set; }

        public instanceController( IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpGet("beat")]
        public async Task<IActionResult> Beat()
        {
            //_configuration.Reload();
            //Console.WriteLine(AppConfig.Configuration.GetSection("Appp").GetChildren());
            Console.WriteLine(jsonSerialize.ObjectToJSON(_configuration.GetSection("Appp").GetChildren()) );
            Console.WriteLine($"Consul健康检查: " + DateTime.Now.ToString());
            
            return Ok();//只是个200 
        }
    }
}

