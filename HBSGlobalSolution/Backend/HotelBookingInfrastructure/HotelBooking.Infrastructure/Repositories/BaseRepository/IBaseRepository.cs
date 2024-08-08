namespace HotelBooking.Infrastructure.Repositories.BaseRepository;

public interface IBaseRepository<T> where T : class
{
    protected internal BookingContext Context { get; set; }

    public async Task CreateAsync(T item)
    {
        await Context.Set<T>().AddAsync(item);
    }

    public async Task CreateRangeAsync(ICollection<T> items)
    {
        await Context.Set<T>().AddRangeAsync(items);
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicateExpression)
    {
        await Context.Set<T>().Where(predicateExpression).ExecuteDeleteAsync();
    }

    public async Task<TResult?> GetItemAsync<TResult>(
        Expression<Func<T, TResult>> selectExpression,
        Expression<Func<T, bool>>? predicateExpression = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Context.Set<T>();

        return predicateExpression is not null
            ? await query.Where(predicateExpression).Select(selectExpression).FirstOrDefaultAsync(cancellationToken)
            : await query.Select(selectExpression).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<TResult>> GetItemsAsync<TResult>(Expression<Func<T, TResult>> selectExpression,
        Expression<Func<T, bool>>? predicateExpression = null,
        Expression<Func<T, object>>? sortPredicateExpression = null,
        int skipItems = default,
        int takeItems = default,
        bool ascending = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Context.Set<T>();

        if (predicateExpression is not null)
            query = query.Where(predicateExpression);

        if (sortPredicateExpression is not null)
            query = ascending ? query.OrderBy(sortPredicateExpression) : query.OrderByDescending(sortPredicateExpression);

        if (takeItems != default)
            query.Skip(skipItems).Take(takeItems);

        return await query.Select(selectExpression).ToListAsync(cancellationToken);
    }

    public async Task<(ICollection<TResult>, int)> GetItemsWIthCountAsync<TResult>(Expression<Func<T, TResult>> selectExpression,
    Expression<Func<T, bool>>? predicateExpression = null,
    Expression<Func<T, object>>? sortPredicateExpression = null,
    int skipItems = default,
    int takeItems = default,
    bool ascending = true,
    CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = Context.Set<T>();

        if (predicateExpression is not null)
            query = query.Where(predicateExpression);

        if (sortPredicateExpression is not null)
            query = ascending ? query.OrderBy(sortPredicateExpression) : query.OrderByDescending(sortPredicateExpression);

        int count = await query.CountAsync(cancellationToken);

        if (takeItems != default)
            query.Skip(skipItems).Take(takeItems);

        ICollection<TResult> items = await query.Select(selectExpression).ToListAsync(cancellationToken);

        return (items, count);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicateExpression, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().AnyAsync(predicateExpression, cancellationToken);
    }
}