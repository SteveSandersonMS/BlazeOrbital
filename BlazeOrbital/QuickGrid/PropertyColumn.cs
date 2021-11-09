using Microsoft.AspNetCore.Components;
using QuickGrid.Infrastructure;
using System.Linq.Expressions;

namespace QuickGrid;

public class PropertyColumn<TGridItem, TProp> : ColumnBase<TGridItem>
{
    private Expression<Func<TGridItem, TProp>>? _cachedProperty;
    private Func<TGridItem, TProp>? _compiledPropertyExpression;

    [Parameter, EditorRequired] public Expression<Func<TGridItem, TProp>> Property { get; set; } = default!;
    [Parameter] public string? Format { get; set; }
    [Parameter] public EventCallback<TGridItem> OnCellClicked { get; set; }

    protected override void OnParametersSet()
    {
        if (_cachedProperty != Property)
        {
            _cachedProperty = Property;
            _compiledPropertyExpression = Property.Compile();

            Func<TGridItem, string?> cellTextFunc;
            if (!string.IsNullOrEmpty(Format))
            {
                if (typeof(IFormattable).IsAssignableFrom(typeof(TProp)))
                {
                    cellTextFunc = item => ((IFormattable?)_compiledPropertyExpression!(item))?.ToString(Format, null);

                }
                else
                {
                    throw new InvalidOperationException($"A '{nameof(Format)}' parameter was supplied, but the type '{typeof(TProp)}' does not implement '{typeof(IFormattable)}'.");
                }
            }
            else
            {
                cellTextFunc = item => _compiledPropertyExpression!(item)?.ToString();
            }

            if (OnCellClicked.HasDelegate)
            {
                CellContent = item => builder =>
                {
                    builder.OpenElement(0, "button");
                    builder.AddAttribute(1, "onclick", () => OnCellClicked.InvokeAsync(item));
                    builder.AddContent(2, cellTextFunc(item));
                    builder.CloseElement();
                };
            }
            else
            {
                CellContent = item => builder => builder.AddContent(0, cellTextFunc(item));
            }
        }

        if (Title is null && _cachedProperty.Body is MemberExpression memberExpression)
        {
            Title = memberExpression.Member.Name;
        }
    }

    internal override bool CanSort => true;

    internal override IQueryable<TGridItem> GetSortedItems(IQueryable<TGridItem> source, bool ascending)
        => ascending ? source.OrderBy(Property) : source.OrderByDescending(Property);
}
