using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public interface IVehicleRepository
    {
        Task<QueryResult<IList<Vehicle>>> GetAllVehicleAsync(BaseFilterQuery filterQuery, bool includeRelated = true);
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true, bool track = false);
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
    }
}