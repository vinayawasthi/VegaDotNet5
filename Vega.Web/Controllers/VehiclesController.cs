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
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public VehiclesController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<VehicleResource>> GetVehicles()
        {
            var vahicles = await this.appDbContext.Vehicles.Include(x => x.Features).ToListAsync();
            return this.mapper.Map<List<Vehicle>, List<VehicleResource>>(vahicles);
        }

        [HttpGet("{id")]
        public async Task<IActionResult> GetVehicle(int id){
            var vehicle = await this.appDbContext.Vehicles.Include(x=>x.Features).Where(x=>x.Id==id).SingleOrDefaultAsync();
            
            if(vehicle==null)
                return NotFound("No vehicle found");
            
            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateVehicles([FromBody] VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);

            var model = await this.appDbContext.Models.Where(x=>x.MakeId==vehicleResource.ModelId).FirstOrDefaultAsync();
            if(model==null){
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }

            var vehicle = this.mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.UtcNow;
            this.appDbContext.Add(vehicle);
            await this.appDbContext.SaveChangesAsync();

            vehicle = await this.appDbContext.Vehicles.Where(x => x.Id == vehicle.Id).FirstAsync();
            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicles(int id,[FromBody] VehicleResource vehicleResource)
        {
            if(!ModelState.IsValid)
               return BadRequest(ModelState);

            var model = await this.appDbContext.Models.Where(x=>x.MakeId==vehicleResource.ModelId).FirstOrDefaultAsync();
            if(model==null){
                ModelState.AddModelError("ModelId", "Invalid Model Id");
                return BadRequest(ModelState);
            }
            
            var vehicle  = await this.appDbContext.Vehicles.AsTracking().Include(x=>x.Features).Where(x=>x.Id == id).SingleOrDefaultAsync();
            if(vehicle==null)
                return NotFound();
                
            vehicle = this.mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.UtcNow;
            this.appDbContext.ChangeTracker.DetectChanges();
            await this.appDbContext.SaveChangesAsync();

            var result = this.mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            var vehicle  = await this.appDbContext.Vehicles.AsTracking().Include(x=>x.Features).Where(x=>x.Id == id).SingleOrDefaultAsync();
            
            if(vehicle==null)
                return NotFound();

            this.appDbContext.Remove(vehicle);
            await this.appDbContext.SaveChanges();
            return Ok(id);
        }
    }
}
