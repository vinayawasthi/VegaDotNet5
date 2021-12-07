using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Web.ApiResources;
using Vega.Web.Persistence;
using Microsoft.EntityFrameworkCore;
using Vega.Web.Models;

namespace Vega.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMakeRepository makeRepository;
        private readonly IModelRepository modelRepository;
        private readonly IFeatureRespository featureRespository;
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IMakeRepository makeRepository,
            IModelRepository modelRepository,
            IFeatureRespository featureRespository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
            this.featureRespository = featureRespository;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var vahicles = await this.vehicleRepository.GetAllVehicleAsync();
            return this.mapper.Map<IList<Vehicle>, IList<VehicleResource>>(vahicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await this.vehicleRepository.GetVehicleAsync(id);

            if (vehicle == null)
                return NotFound("No vehicle found");

            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateVehicles([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var model = await this.modelRepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = this.mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.UtcNow;
            this.vehicleRepository.AddVehicle(vehicle);
            await this.unitOfWork.CompleteAsync();

            vehicle = await this.vehicleRepository.GetVehicleAsync(vehicle.Id, true, false);

            var result = this.mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicles(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await this.modelRepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = await this.vehicleRepository.GetVehicleAsync(id, true ,true);
            if (vehicle == null)
                return NotFound();

            vehicle = this.mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.UtcNow;
            await this.unitOfWork.CompleteAsync();

            var result = this.mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var vehicle = await this.vehicleRepository.GetVehicleAsync(id, false, true);

            if (vehicle == null)
                return NotFound();

            this.vehicleRepository.RemoveVehicle(vehicle);
            
            await this.unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
