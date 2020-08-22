using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Persistence;
using vega.Models;
using AutoMapper;
using vega.Models.Resources;

namespace vega.Controllers
{
    public class MakesController : Controller
    {
        public readonly VegaDbContext Context;
        public readonly IMapper _mapper;
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _mapper=mapper;
            this.Context = context;
        }
        [HttpGet("api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes(){
            var makes= await Context.Makes.Include(x=>x.Models).ToListAsync();
            return _mapper.Map<List<Make>,List<MakeResource>>(makes);
        }
    }
}