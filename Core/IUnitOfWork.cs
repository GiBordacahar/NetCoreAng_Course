using System.Threading.Tasks;

namespace NetCoreAng.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}