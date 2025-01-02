using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {

        // post image :: /api/Image/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequest)
        {
            return View();


        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequest)
        {

            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (imageUploadRequest == null) { }

        }
    }
}
