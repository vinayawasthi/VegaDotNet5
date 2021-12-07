
using Vega.Web.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext dbContext;

        public VehicleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<Vehicle> GetVehicles(bool includeRelated=true)
        {
            if(!includeRelated)
                return this.dbContext.Vehicles.AsQueryable();

            var result = this.dbContext.Vehicles
               .Include(x => x.Model)
                   .ThenInclude(x => x.Make)
               .Include(x => x.Features)
                   .ThenInclude(vf => vf.Feature)
               .AsQueryable();

            return result;
        }

        public async Task<IList<Vehicle>> GetAllVehicleAsync(bool includeRelated = true)
        {
            return await this.GetVehicles(includeRelated).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(int id,bool includeRelated =true,  bool track = false)
        {

            var query = track ? this.GetVehicles(includeRelated).AsTracking() : this.GetVehicles();
            return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            this.dbContext.Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            this.dbContext.Vehicles.Remove(vehicle);
        }
    }
}