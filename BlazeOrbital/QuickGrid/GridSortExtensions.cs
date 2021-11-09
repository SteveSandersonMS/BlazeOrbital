using System.Linq.Expressions;

namespace QuickGrid;

public static class GridSortExtensions
{
    public static SortBy<T> SortByAscending<T, U>(this IQueryable<T> _, Expression<Func<T, U>> keySelector)
        => SortBy<T>.Ascending(keySelector);

    public static SortBy<T> SortByDecending<T, U>(this IQueryable<T> _, Expression<Func<T, U>> keySelector)
        => SortBy<T>.Descending(keySelector);
}

public struct SortBy<T>
{
    private int _count;
    private Func<IQueryable<T>, bool, IOrderedQueryable<T>> _first;
    private Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>>[] _then = new Func<IOrderedQueryable<T>, bool, IOrderedQueryable<T>>[10];

    internal SortBy(Func<IQueryable<T>, bool, IOrderedQueryable<T>> first)
    {
        _first = first;
        _count = 0;
    }

    public static SortBy<T> Ascending<U>(Expression<Func<T, U>> expression)
        => new SortBy<T>((queryable, asc) => asc ? queryable.OrderBy(expression) : queryable.OrderByDescending(expression));

    public static SortBy<T> Descending<U>(Expression<Func<T, U>> expression)
        => new SortBy<T>((queryable, asc) => asc ? queryable.OrderByDescending(expression) : queryable.OrderBy(expression));

    public SortBy<T> ThenAscending<U>(Expression<Func<T, U>> expression)
    {
        _then[_count++] = (queryable, asc) => asc ? queryable.ThenBy(expression) : queryable.ThenByDescending(expression);
        return this;
    }

    public SortBy<T> ThenDescending<U>(Expression<Func<T, U>> expression)
    {
        _then[_count++] = (queryable, asc) => asc ? queryable.ThenByDescending(expression) : queryable.ThenBy(expression);
        return this;
    }

    internal IOrderedQueryable<T> Apply(IQueryable<T> queryable, bool ascending)
    {
        var orderedQueryable = _first(queryable, ascending);
        for (var i = 0; i < _count; i++)
        {
            orderedQueryable = _then[i](orderedQueryable, ascending);
        }
        return orderedQueryable;
    }
}

public enum Align
{
    Left,
    Center,
    Right,
}
