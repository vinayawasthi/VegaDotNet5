
using Vega.Web.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using Vega.Web.Extentions;

namespace Vega.Web.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext dbContext;

        public VehicleRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<Vehicle> GetVehicles(BaseFilterQuery filterQuery, bool includeRelated = true)
        {
            IQueryable<Vehicle> query;
            if (!includeRelated)
            {
                query = this.dbContext.Vehicles.AsQueryable();
            }
            else
            {
                query = this.dbContext.Vehicles
                   .Include(x => x.Model)
                       .ThenInclude(x => x.Make)
                   .Include(x => x.Features)
                       .ThenInclude(vf => vf.Feature)
                   .AsQueryable();
            }

            if (filterQuery != null && filterQuery is ISortObject)
            {
                var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
                {
                    ["make"] = v => v.Model.Make.Name,
                    ["model"] = v => v.Model.Name,
                    ["person"] = v => v.PersonName,
                    ["last_update"] = v => v.LastUpdate
                };

                query = query.ApplyOrdering(filterQuery, columnMap);
            }
            return query;
        }

        public async Task<QueryResult<IList<Vehicle>>> GetAllVehicleAsync(BaseFilterQuery filterQuery, bool includeRelated = true)
        {
            var query = this.GetVehicles(filterQuery, includeRelated);

            QueryResult<IList<Vehicle>> result = new QueryResult<IList<Vehicle>>()
            {
                TotalItems = await query.CountAsync(),
                Items = await query.ApplyPaging(filterQuery.PageIndex, filterQuery.PageSize).ToListAsync()
            };

            return result;
        }

        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true, bool track = false)
        {
            var query = track ? this.GetVehicles(null, includeRelated).AsTracking() : this.GetVehicles(null, includeRelated);
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