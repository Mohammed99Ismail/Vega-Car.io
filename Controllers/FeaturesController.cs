using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext Context;
        public FeaturesController(VegaDbContext context)
        {
            this.Context = context;
        }
        [HttpGet("api/features")]
        public async Task<IEnumerable<Features>> GetFeatures(){
            return await Context.Features.ToListAsync();
        }
    }
}