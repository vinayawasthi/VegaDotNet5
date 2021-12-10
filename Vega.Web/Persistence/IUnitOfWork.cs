using System.Threading.Tasks;

namespace Vega.Web.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}