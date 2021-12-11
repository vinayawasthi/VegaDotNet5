using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.ApiResources;
using Vega.Web.Models;
using Vega.Web.Persistence;

namespace Vega.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMakeRepository makeRepository;
        private readonly IModelRepository modelRepository;
        private readonly IFeatureRespository featureRespository;
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVehicleRepository vehicleRepository,
            IMakeRepository makeRepository,
            IModelRepository modelRepository,
            IFeatureRespository featureRespository            
           )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
            this.featureRespository = featureRespository;
            this.vehicleRepository = vehicleRepository;

            System.Threading.Thread.Sleep(5000);
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<QueryResult<IList<VehicleResource>>> GetVehicles()
        {
            var vahicles = await vehicleRepository.GetVehiclesAsync(new BaseFilterQuery()
            {
                SortBy = "",
                IsSortAscending = true,
                PageIndex = 1,
                PageSize = 100
            });

            QueryResult<IList<VehicleResource>> result = new QueryResult<IList<VehicleResource>>()
            {
                TotalItems = vahicles.TotalItems,
                Items = mapper.Map<IList<Vehicle>, IList<VehicleResource>>(vahicles.Items)
            };

            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicleAsync(id);

            if (vehicle == null)
                return NotFound("No vehicle found");

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateVehicles([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var model = await modelRepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.UtcNow;
            vehicleRepository.AddVehicle(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await vehicleRepository.GetVehicleAsync(vehicle.Id, true, false);

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicles(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = await modelRepository.GetModelAsync(vehicleResource.ModelId);
            if (model == null)
            {
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = await vehicleRepository.GetVehicleAsync(id, true, true);
            if (vehicle == null)
                return NotFound();

            vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.UtcNow;
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var vehicle = await vehicleRepository.GetVehicleAsync(id, false, true);

            if (vehicle == null)
                return NotFound();

            vehicleRepository.RemoveVehicle(vehicle);

            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
