using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreAng.Core;
using NetCoreAng.Core.Models;

namespace NetCoreAng.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {   
        private readonly AppDbContext context;

        public PhotoRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleID) {
            return await context.Photos
                            .Where(p => p.VehicleId == vehicleID)
                            .ToListAsync();
        }
        
    }
}