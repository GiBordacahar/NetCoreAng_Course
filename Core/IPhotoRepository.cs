using System.Collections.Generic;
using System.Threading.Tasks;
using NetCoreAng.Core.Models;

namespace NetCoreAng.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}