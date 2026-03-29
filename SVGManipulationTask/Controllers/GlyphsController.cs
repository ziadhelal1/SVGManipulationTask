using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SVGManipulationTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlyphsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public GlyphsController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [HttpGet]
        public IActionResult GetGlyphs()
        {
            
            var glyphsPath = Path.Combine(_env.WebRootPath, "glyphs");

            if (!Directory.Exists(glyphsPath))
                return NotFound("Folder not found");

            var files = Directory.GetFiles(glyphsPath, "*.svg")
                .Select(filePath => new
                {
                    Id = Path.GetFileNameWithoutExtension(filePath), 
                    FileName = Path.GetFileName(filePath),           
                    Url = $"{Request.Scheme}://{Request.Host}/glyphs/{Path.GetFileName(filePath)}"
                });

            return Ok(files);
        }
    }
}
