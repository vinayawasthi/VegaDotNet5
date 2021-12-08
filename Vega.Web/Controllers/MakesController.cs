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
    public class MakesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMakeRepository makeRepository;
        private readonly IModelRepository modelRepository;

        public MakesController(IUnitOfWork unitOfWork, IMapper mapper
        , IMakeRepository makeRepository
        , IModelRepository modelRepository
        )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await this.makeRepository.GetAllMakeAsync();
            return mapper.Map<IList<Make>, IList<MakeResource>>(makes);
        }

        [HttpGet("{makeId}")]
        public async Task<MakeResource> GetMake(int makeId)
        {
            var make = await this.makeRepository.GetMakeAsync(makeId, true);
            return mapper.Map<Make, MakeResource>(make);
        }

        [HttpGet("{makeId}/models")]
        public async Task<IEnumerable<ModelResource>> GetModelsByMakeId(int makeId)
        {
            var models = await this.modelRepository.GetAllModelByMakeIdAsync(makeId);
            return mapper.Map<IList<Model>, IList<ModelResource>>(models);
        }
    }
}
