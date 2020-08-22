using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Models.Resources;
using vega.Models;

namespace vega.Persistence
{
    public interface IVehicleRepasitory
    {
        void Add(Vehicle vehicle);
        Task<Vehicle> GetVehicle(int id, bool includedRelated = true);
        Task<List<Vehicle>> GetVehicles(VehicleQueryResource filter);
        void Remove(Vehicle vehicle);
    }
}