using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
namespace CaseStudyQuitQ.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase {

        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        public ImageController() {
            if (!Directory.Exists(_uploadFolder)) {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file) {
            if (file == null || file.Length == 0) {
                return BadRequest("No file uploaded");
            }

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_uploadFolder, fileName);

            using(var stream=new FileStream(filePath, FileMode.Create)) {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"/uploads/{fileName}";
            return Ok(new { ImageUrl = imageUrl });

        }
    }
}
