using System.Threading.Tasks;

namespace Vega.Web.Persistence
{
    public class UnitOfWOrk : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        public UnitOfWOrk(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }
    }
}