using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // post image :: /api/Image/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequest)
        {
            ValidateFileUpload(imageUploadRequest);



            if (ModelState.IsValid)
            {

                // convert Dto to Domaim model

                var imageDomainModel = new Image
                {
                    File = imageUploadRequest.File,
                    FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
                    FileSizeInBytes = imageUploadRequest.File.Length,
                    FileName = imageUploadRequest.File.FileName,
                    FileDescription = imageUploadRequest.FileDescription,

                };

                // use reposiytiry

                await imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequest)
        {

            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(imageUploadRequest.File.FileName)) ) {

                ModelState.AddModelError("file", "Unsupported file extension");
            
            }

            if (imageUploadRequest.File.Length >10485760)
            {
                ModelState.AddModelError("file", "File size more than 10 MB , Please upload smaller size");

            }

        }
    }
}
