using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.Models;

namespace Vega.Web.Persistence
{
    public interface IVehicleRepository
    {
        Task<QueryResult<IList<Vehicle>>> GetVehiclesAsync(BaseFilterQuery filterQuery, bool includeRelated = true);
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true, bool track = false);
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
    }
}