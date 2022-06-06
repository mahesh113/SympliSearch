using Microsoft.AspNetCore.Mvc;
using SympliDevelopment.Api.Interface;

namespace SympliDevelopment.Api.Controllers
{
    /// <summary>
    /// Google Search Controller.
    /// </summary>
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
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetResult([FromQuery] string url, [FromQuery] string keywords)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("Url not provided");
            else if (string.IsNullOrEmpty(keywords))
                return BadRequest("Enter Keywords");
            else if(!url.ToLower().Contains("http") ) // checks both http/https
                return BadRequest("Please enter URL in format -  http(s)://www.yoursite.com.au");
            var resp =  await _gs.Search(url, keywords);

            return Ok(resp);
        }
    
  }
}