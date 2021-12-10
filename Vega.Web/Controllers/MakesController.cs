using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Web.Models;
using Vega.Web.ApiResources;
using Vega.Web.Persistence;
using System.Linq;

namespace Vega.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MakesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;       
        private readonly IMakeRepository makeRepository;
        private readonly IModelRepository modelRepository;

        public MakesController(
            IMapper mapper,
            IUnitOfWork unitOfWork, 
            IMakeRepository makeRepository,
            IModelRepository modelRepository
        )
        {   
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;            
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await makeRepository.GetAllMakeAsync();
            return mapper.Map<IList<Make>, IList<MakeResource>>(makes);
        }

        [HttpGet("{makeId}")]
        public async Task<MakeResource> GetMake(int makeId)
        {
            var make = await makeRepository.GetMakeAsync(makeId, true);
            return mapper.Map<Make, MakeResource>(make);
        }

        [HttpGet("{makeId}/models")]
        public async Task<IEnumerable<ModelResource>> GetModelsByMakeId(int makeId)
        {
            var models = await modelRepository.GetAllModelByMakeIdAsync(makeId);
            return mapper.Map<IList<Model>, IList<ModelResource>>(models);
        }
    }
}
