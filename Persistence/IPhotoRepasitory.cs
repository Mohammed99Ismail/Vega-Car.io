using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Persistence
{
    public interface IPhotoRepasitory
    {
         Task<IEnumerable<Photo>> GetPhotos(int id);
    }
}