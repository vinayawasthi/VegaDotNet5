using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public interface IModelRepository
    {
        Task<IList<Model>> GetAllModelAsync();

        Task<Model> GetModelAsync(int id, bool track = false);
        Task<IList<Model>> GetAllModelByMakeIdAsync(int makeId, bool track = false);
    }
}