using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreAng.Controllers.Resources;
using NetCoreAng.Core;
using NetCoreAng.Core.Models;
using NetCoreAng.Persistence;

namespace NetCoreAng.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IWebHostEnvironment host;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly PhotoRepository photoRepository;
        public PhotosController(IWebHostEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, 
        IMapper mapper, IOptionsSnapshot<PhotoSettings> options,
        PhotoRepository photoRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.host = host;
            this.photoSettings = options.Value;
            this.photoRepository = photoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            if (file == null)
                return BadRequest("Null File");

            if (file.Length == 0)
                return BadRequest("Empty File");
            
            if (file.Length > photoSettings.MaxBytes)
                return BadRequest("Max file size excedeed");

            if (!photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid file type");


            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            //System.Drawing. generate a thumbnail
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);

            await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos (int vehicleId) {
            var photos =  await photoRepository.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }
}