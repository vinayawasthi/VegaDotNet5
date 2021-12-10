using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Web.Models;

namespace Vega.Web.Persistence
{
    public class MakeRepository : IMakeRepository
    {
        private readonly AppDbContext dbContext;

        public MakeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected IQueryable<Make> GetMakes(bool includeRelated = true)
        {
            if (!includeRelated)
                return this.dbContext.Makes.AsQueryable();

            var result = this.dbContext.Makes.Include(x => x.Models).AsQueryable();
            return result;
        }


        public async Task<IList<Make>> GetAllMakeAsync(bool includeRelated = true)
        {
            return await this.GetMakes(includeRelated).ToListAsync();
        }

        public async Task<Make> GetMakeAsync(int id, bool includeRelated = true, bool track = false)
        {
            var query = track ? this.GetMakes(includeRelated).AsTracking() : this.GetMakes();
            return await query.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}