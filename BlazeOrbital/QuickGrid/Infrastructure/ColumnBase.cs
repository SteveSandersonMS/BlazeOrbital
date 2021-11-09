using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace QuickGrid.Infrastructure;

public abstract class ColumnBase<TGridItem> : ComponentBase
{
    private readonly static RenderFragment<TGridItem> EmptyChildContent = _ => builder => { };

    [CascadingParameter] internal Grid<TGridItem>.AddColumnCallback AddColumn { get; set; } = default!;

    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Class { get; set; }
    [Parameter] public Align Align { get; set; }
    [Parameter] public RenderFragment? ColumnOptions { get; set; }

    internal RenderFragment HeaderContent { get; }

    protected internal RenderFragment<TGridItem> CellContent { get; protected set; } = EmptyChildContent;

    public ColumnBase()
    {
        HeaderContent = __builder => __builder.AddContent(0, Title);
    }

    internal virtual bool CanSort => false;

    internal virtual IQueryable<TGridItem> GetSortedItems(IQueryable<TGridItem> source, bool ascending) => source;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        AddColumn(this);
    }
}
