using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Models.Resources;
using vega.Persistence;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepasitory repasitory;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IUnitOfWork unitOfWork, IMapper mapper, IVehicleRepasitory repasitory)
        {
            this.unitOfWork = unitOfWork;
            this.repasitory = repasitory;
            this.mapper = mapper;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVechicle(int id)
        {
            var vehicle = await repasitory.GetVehicle(id);

            if (vehicle == null)
                return NotFound();
            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleResource>> GetVechicles(VehicleQueryResource filter)
        {
            var filterX=mapper.Map<VehicleQueryResource,VehicleQuery>(filter);
            var vehicles = await repasitory.GetVehicles(filter);
            /*if (vehicle == null)
                return NotFound();*/
            var vehicleResource = mapper.Map<IEnumerable<Vehicle>,IEnumerable<VehicleResource>>(vehicles);

            return vehicleResource;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateVechicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            DateTime date = DateTime.Today;
            vehicle.LastUpdate = date;
            repasitory.Add(vehicle);
            await unitOfWork.Complete();

            vehicle = await repasitory.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateVechicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repasitory.GetVehicle(id);
            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);

            if (vehicle == null)
                return NotFound();

            DateTime date = DateTime.Today;
            vehicle.LastUpdate = date;

            await unitOfWork.Complete();
            
            vehicle= await repasitory.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteVechicle(int id)
        {

            var vehicle = await repasitory.GetVehicle(id, includedRelated: false);
            if (vehicle == null)
                return NotFound();

            repasitory.Remove(vehicle);
            await unitOfWork.Complete();

            return Ok(id);
        }

    }
}