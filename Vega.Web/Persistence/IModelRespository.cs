using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.Models;

namespace Vega.Web.Persistence
{
    public interface IModelRepository
    {
        Task<IList<Model>> GetAllModelAsync();

        Task<Model> GetModelAsync(int id, bool track = false);
        Task<IList<Model>> GetAllModelByMakeIdAsync(int makeId, bool track = false);
    }
}