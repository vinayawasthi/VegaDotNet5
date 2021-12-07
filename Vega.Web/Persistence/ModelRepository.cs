using Vega.Web.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using Vega.Web.Models;
using System.Collections.Generic;

namespace Vega.Web.Persistence
{
    public class ModelRepository : IModelRepository
    {
        private readonly AppDbContext dbContext;

        public ModelRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<Model> GetModels()
        {
            var result = this.dbContext.Models.AsQueryable();

            return result;
        }

        public async Task<IList<Model>> GetAllModelAsync()
        {
            return await this.GetModels().ToListAsync();
        }

        public async Task<Model> GetModelAsync(int id, bool track = false)
        {
            var query = track ? this.GetModels().AsTracking() : this.GetModels();
            return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IList<Model>> GetAllModelByMakeIdAsync(int makeId, bool track = false)
        {
            var query = track ? this.GetModels().AsTracking() : this.GetModels();
            return await query.Where(x => x.MakeId == makeId).ToListAsync();
        }
    }
}