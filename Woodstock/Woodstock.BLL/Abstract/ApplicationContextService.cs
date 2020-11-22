using Microsoft.EntityFrameworkCore;
using Woodstock.DAL;

namespace Woodstock.BLL.Abstract
{
    public abstract class ApplicationContextService<TSource> where TSource : class
    {
        protected readonly WoodstockDbContext Context;
        protected readonly DbSet<TSource> Set;

        protected ApplicationContextService(WoodstockDbContext context)
        {
            Context = context;
            Set = context.Set<TSource>();
        }
    }
}
