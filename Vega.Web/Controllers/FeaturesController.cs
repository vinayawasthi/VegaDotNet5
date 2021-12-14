using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.ApiResources;
using Vega.Web.Models;
using Vega.Web.Persistence;

namespace Vega.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;       
        private readonly IFeatureRespository featureRespository;
        
        public FeaturesController(
            IMapper mapper,
            IUnitOfWork unitOfWork, 
            IFeatureRespository featureRespository
        )
        {   
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;            
            this.featureRespository = featureRespository;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<FeatureResource>> GetFeatures()
        {
            var features = await featureRespository.GetAllFeatureAsync();
            return mapper.Map<IList<Feature>, IList<FeatureResource>>(features);
        }

        [HttpGet("{id}")]
        public async Task<FeatureResource> GetFeature(int id)
        {
            var feature = await featureRespository.GetFeatureAsync(id, false );
            return mapper.Map<Feature, FeatureResource>(feature);
        }
    }
}
