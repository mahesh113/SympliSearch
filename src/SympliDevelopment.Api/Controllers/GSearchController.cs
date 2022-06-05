using Microsoft.AspNetCore.Mvc;
using SympliDevelopment.Api.Interface;

namespace SympliDevelopment.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class GSearchController : ControllerBase
  {
        private readonly IGSearch _gs; // google search service
        public GSearchController(IGSearch s)
        {
            _gs = s;
        }
        [HttpGet("keywords")]
        public async Task<IActionResult> GetResult([FromQuery] string url, [FromQuery] string keywords)
        {
            var resp =  await _gs.Search(url, keywords);
            return Ok(keywords);
        }
    
  }
}