using System.Threading.Tasks;

namespace vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public VegaDbContext Context { get; }
        public UnitOfWork(VegaDbContext context)
        {
            this.Context = context;

        }
        public async Task Complete()
        {
            await Context.SaveChangesAsync();
        }
    }
    public interface IUnitOfWork
    {
        Task Complete();
    }
}