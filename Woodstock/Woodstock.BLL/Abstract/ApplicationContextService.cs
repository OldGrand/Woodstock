using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Woodstock.DAL;

namespace Woodstock.BLL.Abstract
{
    public abstract class ApplicationContextService<TSource> where TSource : class
    {
        protected readonly WoodstockDbContext Context;
        protected readonly DbSet<TSource> Set;
        protected readonly IMapper Mapper;

        protected ApplicationContextService(WoodstockDbContext context, IMapper mapper)
        {
            Context = context;
            Set = context.Set<TSource>();
            Mapper = mapper;
        }
    }
}
