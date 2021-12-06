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
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public MakesController(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        [HttpGet("")]
        [HttpGet("list")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await appDbContext.Makes.Include(x=>x.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }

        [HttpGet("{makeId}")]
        public async Task<MakeResource> GetMake(int makeId)
        {
            var make = await appDbContext.Makes.Where(x => x.Id == makeId).Include(x => x.Models).FirstOrDefaultAsync();
            return mapper.Map<Make, MakeResource>(make);
        }

        [HttpGet("{makeId}/models")]       
        public async Task<IEnumerable<ModelResource>> GetModelsByMakeId(int makeId)
        {
            var models = await appDbContext.Models.Where(x=> x.MakeId == makeId).ToListAsync();
            return mapper.Map<List<Model>, List<ModelResource>>(models);
        }
    }
}
