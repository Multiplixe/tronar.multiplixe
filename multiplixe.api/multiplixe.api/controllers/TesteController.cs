using Microsoft.AspNetCore.Mvc;

namespace multiplixe.api.controllers
{
    [Route("teste")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}