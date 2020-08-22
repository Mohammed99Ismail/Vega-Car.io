using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Models.Resources;
using System.Linq.Expressions;
using System;
using vega.vscode.Extensions;

namespace vega.Persistence
{
    public class VehicleRepasitory : IVehicleRepasitory
    {
        private readonly VegaDbContext Context;
        public VehicleRepasitory(VegaDbContext context)
        {
            this.Context = context;

        }
        public async Task<Vehicle> GetVehicle(int id,bool includedRelated=true)
        {
            if(!includedRelated)
            return await Context.vehicles.FindAsync(id);

            return await Context.vehicles.Include(v => v.Features)
            .ThenInclude(VehicleFeature => VehicleFeature.Feature)
            .Include(v => v.Model).ThenInclude(v => v.Make)
            .SingleOrDefaultAsync(v => v.Id == id);
        }
        public void Add(Vehicle vehicle){
            Context.vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle){
            Context.vehicles.Remove(vehicle);
        }
        public async Task<List<Vehicle>> GetVehicles(VehicleQueryResource queryObj)
        {
            var query=Context.vehicles.Include(v => v.Model)
            .ThenInclude(v => v.Make).Include(v => v.Features)
            .ThenInclude(VehicleFeature => VehicleFeature.Feature).AsQueryable();
        
        if(queryObj.MakeId.HasValue)
            query=query.Where(v=>v.Model.MakeId==queryObj.MakeId.Value);
        if(!string.IsNullOrWhiteSpace(queryObj.SortBy))
        {
        var colmunsMap=new Dictionary<string,Expression<Func<Vehicle,object>>>(){
            ["make"]=v=>v.Model.Make.Name,
            ["model"]=v=>v.Model.Name,
            ["name"]=v=>v.ContactName,
            ["id"]=v=>v.Id,
        };
        query=query.ApplyingOrdering(queryObj,colmunsMap);

        }
            query=query.ApplyingPaging(queryObj);
            return await query.ToListAsync();
            
        }

    }
}