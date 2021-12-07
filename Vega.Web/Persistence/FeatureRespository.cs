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
    public class FeatureRespository : IFeatureRespository
    {
        private readonly AppDbContext dbContext;

        public FeatureRespository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<Feature> GetFeatures()
        {
            var result = this.dbContext.Features.AsQueryable();

            return result;
        }

        public async Task<IList<Feature>> GetAllFeatureAsync()
        {
            return await this.GetFeatures().ToListAsync();
        }

        public async Task<Feature> GetFeatureAsync(int id, bool track = false)
        {
            var query = track ? this.GetFeatures().AsTracking() : this.GetFeatures();
            return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}