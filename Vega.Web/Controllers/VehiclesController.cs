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
            var vahicles = await this.appDbContext.Vehicles.ToListAsync();
            return this.mapper.Map<List<Vehicle>, List<VehicleResource>>(vahicles);
        }

        [HttpPost()]
        public async Task<Vehicle> CreateVehicles([FromBody] VehicleResource vehicleResource)
        {
            return this.mapper.Map<VehicleResource, Vehicle> (vehicleResource);
        }
    }
}
