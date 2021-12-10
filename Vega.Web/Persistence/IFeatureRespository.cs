using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.Models;

namespace Vega.Web.Persistence
{
    public interface IFeatureRespository
    {
        Task<IList<Feature>> GetAllFeatureAsync();

        Task<Feature> GetFeatureAsync(int id, bool track = false);
    }
}