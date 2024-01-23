﻿using Course.Services.PhotoStock.Dtos;
using Course.Shared.ControllerBases;
using Course.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Course.Services.PhotoStock.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave (IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo == null || photo.Length < 1)
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;

                var photoDto = new PhotoDto { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto,200));
            }
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));            
        }

    }
}