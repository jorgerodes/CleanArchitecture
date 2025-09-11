using System.Linq.Expressions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Infrastructure.Repository;

internal abstract class Repository<TEntity, TEntityId>
where TEntity : Entity<TEntityId>
where TEntityId : class
{

    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(
        TEntityId id,
        CancellationToken cancellationToken)
    {
        return await DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public IQueryable<TEntity> ApplySpecification(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return SpecificationsEvaluator<TEntity, TEntityId>
                .GetQuery(DbContext.Set<TEntity>().AsQueryable(), spec);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return await ApplySpecification(spec).CountAsync();
    }

    //GetPaginationAsync
    // pagedresults
    public async Task<PagedResults<TEntity, TEntityId>> GetPaginationAsync
    (
        Expression<Func<TEntity, bool>>? precicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes,
        int page,
        int pageSize,
        string orderBy,
        bool ascending,
        bool disableTracking = true
    )
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (precicate != null)
        {
            query = query.Where(precicate);
        }

        if (includes != null)
        {
            query = includes(query);
        }


        var skipAmount = pageSize * (page - 1);
        var TotalNumberOfRecords = await query.CountAsync();

        var records = new List<TEntity>();

        if (!string.IsNullOrEmpty(orderBy))
        {
            var propertyInfo = typeof(TEntity).GetProperty(orderBy);
            if (propertyInfo != null)
            {
                records = await query.Skip(skipAmount).Take(pageSize).ToListAsync();
            }
        }
        else
        {
            records = await query.OrderByPropertyOrField(orderBy, ascending)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToListAsync();
        }
        var mod = TotalNumberOfRecords % pageSize;

        var totalPageCount = (TotalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

        return new PagedResults<TEntity, TEntityId>
        {
            Results = records,
            PageNumber = page,
            PageSize = pageSize,
            TotalNumberOfRecords = TotalNumberOfRecords,
            TotalNumberOfPages = (int)Math.Ceiling((double)TotalNumberOfRecords / pageSize)
            
        };
    }

}
