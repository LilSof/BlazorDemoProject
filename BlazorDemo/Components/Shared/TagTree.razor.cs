using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Components.Shared
{
    public partial class TagTree<TItem> : ComponentBase
    {
        [Parameter] public List<TItem> Items { get; set; }
        [Parameter] public Func<TItem, string> ItemText { get; set; }
        [Parameter] public Func<TItem, List<TItem>> GetChildren { get; set; }
        [Parameter] public List<TItem> SelectedItems { get; set; } = new();
        [Parameter] public EventCallback<List<TItem>> OnSelectionChanged { get; set; }

        [Parameter] public EventCallback<TItem> OnDelete { get; set; }
        [Parameter] public EventCallback<(TItem item, string newName)> OnRename { get; set; }
        [Parameter] public EventCallback<(TItem parent, string childName)> OnAddChild { get; set; }
        [Parameter] public Func<TItem, Guid> GetId { get; set; }

        private TItem EditingItem;
        private string EditText;

        private TItem AddingChildTo;
        private string NewChildName;

        private async Task HandleEditKey(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && EditingItem != null)
            {
                await OnRename.InvokeAsync((EditingItem, EditText));
                EditingItem = default;
            }
        }

        private async Task HandleAddChildKey(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && AddingChildTo != null && !string.IsNullOrWhiteSpace(NewChildName))
            {
                await OnAddChild.InvokeAsync((AddingChildTo, NewChildName));
                AddingChildTo = default;
                NewChildName = string.Empty;
            }
        }

        private void StartEditing(TItem item)
        {
            EditingItem = item;
            EditText = ItemText(item);
        }

        private void StartAddingChild(TItem item)
        {
            AddingChildTo = item;
            NewChildName = string.Empty;
        }

        private async Task DeleteItem(TItem item)
        {
            await OnDelete.InvokeAsync(item);
        }
    }
}
