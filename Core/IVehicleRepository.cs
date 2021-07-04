using System.Threading.Tasks;
using NetCoreAng.Core.Models;

namespace NetCoreAng.Core
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj);
        void Remove(Vehicle vehicle);
    }
}