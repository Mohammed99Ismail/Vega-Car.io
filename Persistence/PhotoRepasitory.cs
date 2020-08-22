using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class PhotoRepasitory:IPhotoRepasitory
    {
        private readonly VegaDbContext context;
        public PhotoRepasitory(VegaDbContext context)
        {
            this.context = context;

        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await context.Photos.Where(x=>x.vehicleId==vehicleId).ToListAsync();
        }
    }
}