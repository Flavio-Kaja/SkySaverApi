namespace SkySaver.Services;

using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using SkySaver.Domain;
using SkySaver.Databases;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
public interface IGenericRepository<TEntity> : ISkySaverScopedService
    where TEntity : IAuditable
{
    IQueryable<TEntity> Query();
    Task<TEntity?> GetByIdOrDefault(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity> GetById(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRange(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entity);
    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<TResult?> GetByIdOrDefault<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);
    Task<TEntity> GetById(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task<TResult> GetById<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default);
}

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IAuditable
{
    private readonly SpecificationEvaluator _specificationEvaluator = SpecificationEvaluator.Default;
    private readonly SkySaverDbContext _dbContext;

    protected GenericRepository(SkySaverDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public virtual IQueryable<TEntity> Query() => _dbContext.Set<TEntity>();

    public virtual async Task<TEntity> GetByIdOrDefault(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    {
        return withTracking
            ? await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            : await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetById(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdOrDefault(id, withTracking, cancellationToken);

        return entity == null ? throw new NotFoundException($"{typeof(TEntity).Name} with an id '{id}' was not found.") : entity;
    }

    public virtual async Task<bool> Exists(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>()
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }

    public virtual async Task<List<TResult>> ListAsync<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null
            ? queryResult
            : specification.PostProcessingAction(queryResult).ToList();
    }

    public virtual async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null
            ? queryResult
            : specification.PostProcessingAction(queryResult).ToList();
    }

    public virtual async Task<TEntity?> GetByIdOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TResult?> GetByIdOrDefault<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TEntity> GetById(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdOrDefault(specification, cancellationToken);

        return entity == null
            ? throw new NotFoundException($"{typeof(TEntity).Name} with a query '{specification.Query}' was not found.")
            : entity;
    }

    public virtual async Task<TResult> GetById<TResult>(ISpecification<TEntity, TResult> specification, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdOrDefault(specification, cancellationToken);

        if (entity == null)
            throw new NotFoundException($"{typeof(TEntity).Name} with a query '{specification.Query}' was not found.");

        return entity;
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification, bool evaluateCriteriaOnly = false)
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification, evaluateCriteriaOnly);
    }

    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<TEntity, TResult> specification)
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
    }
}
