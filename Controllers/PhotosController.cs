using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Models.Resources;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/vehicles/{VehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly string[] ACCEPTED_FILE_TYPES=new []{ ".jpeg",".jpg",".png" };
        private readonly int MAX_BYTES=10*1024*1024;
        private readonly IHostingEnvironment host;
        private readonly IMapper mapper;
        private readonly IVehicleRepasitory repasitory;
        private readonly IPhotoRepasitory photoRepasitory;
        private readonly IUnitOfWork UnitOfWork;
        public PhotosController(IHostingEnvironment host, IMapper mapper,
         IVehicleRepasitory repasitory, IUnitOfWork UnitOfWork,IPhotoRepasitory photoRepasitory)
        {
            this.photoRepasitory=photoRepasitory;
            this.UnitOfWork = UnitOfWork;
            this.repasitory = repasitory;
            this.mapper = mapper;
            this.host = host;
        }
        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId){
            var photo=await photoRepasitory.GetPhotos(vehicleId);
            return mapper.Map<IEnumerable<Photo>,IEnumerable<PhotoResource>>(photo);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file,int VehicleId){
            var vehicle=await repasitory.GetVehicle(VehicleId,includedRelated:false);
            var UploadsFolderpath=Path.Combine(host.WebRootPath,"uploads");

            if(!ACCEPTED_FILE_TYPES.Any(s=>s==Path.GetExtension(file.FileName)))
             return BadRequest("Invalid type file.");

            if(file.Length>MAX_BYTES)
            return BadRequest("Max file size exceeded.");

            if(file.Length<=0)
            return BadRequest("Empty file.");

            if(file==null)
            return BadRequest("Null File.");

            if(!Directory.Exists(UploadsFolderpath))
                Directory.CreateDirectory(UploadsFolderpath);

            var fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            var filePath=Path.Combine(UploadsFolderpath,fileName);

            using(var stream=new FileStream(filePath,FileMode.Create)){
                await file.CopyToAsync(stream);
            }
            
            var photo=new Photo{ FileName=fileName };
            vehicle.Photos.Add(photo);
            await UnitOfWork.Complete();

            return Ok(mapper.Map<Photo,PhotoResource>(photo));
        }

    }
}