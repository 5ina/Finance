using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace NetCommunitySolution.EntityFramework.Repositories
{
    public abstract class NetCommunitySolutionRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<NetCommunitySolutionDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected NetCommunitySolutionRepositoryBase(IDbContextProvider<NetCommunitySolutionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class NetCommunitySolutionRepositoryBase<TEntity> : NetCommunitySolutionRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected NetCommunitySolutionRepositoryBase(IDbContextProvider<NetCommunitySolutionDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
