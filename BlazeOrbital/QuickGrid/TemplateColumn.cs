using Microsoft.AspNetCore.Components;
using QuickGrid.Infrastructure;

namespace QuickGrid;

public class TemplateColumn<TGridItem> : ColumnBase<TGridItem>
{
    private readonly static RenderFragment<TGridItem> EmptyChildContent = _ => builder => { };

    [Parameter] public RenderFragment<TGridItem> ChildContent { get; set; } = EmptyChildContent;
    [Parameter] public Func<IQueryable<TGridItem>, SortBy<TGridItem>>? SortBy { get; set; }

    protected override void OnParametersSet()
    {
        CellContent = ChildContent;
    }

    internal override bool CanSort => SortBy != null;

    internal override IQueryable<TGridItem> GetSortedItems(IQueryable<TGridItem> source, bool ascending)
        => SortBy == null ? source : SortBy(source).Apply(source, ascending);
}
