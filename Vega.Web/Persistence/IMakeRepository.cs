using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.Models;

namespace Vega.Web.Persistence
{
    public interface IMakeRepository
    {
        Task<IList<Make>> GetAllMakeAsync(bool includeRelated = true);

        Task<Make> GetMakeAsync(int id, bool includeRelated = true, bool track = false);
    }
}