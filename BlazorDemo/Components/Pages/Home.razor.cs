using BlazorDemo.Models;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        private List<Tag> Tags = new();
        private List<ProjectHighlight> _projects = new();

        protected override void OnInitialized()
        {
            Tags = new()
            {
                new Tag
                {
                    Id = Guid.Parse("35f7d762-8ae8-450f-ae4a-cd01e0b77ae1"),
                    Name = "Frontend",
                    Children = new()
                    {
                        new Tag
                        {
                            Id = Guid.Parse("f59fa384-73b9-4f99-99dd-7eabf32e861d"),
                            ParentId = Guid.Parse("35f7d762-8ae8-450f-ae4a-cd01e0b77ae1"),
                            Name = "Blazor(Server\\WASM)",
                            Children = new()
                            {
                                new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("f59fa384-73b9-4f99-99dd-7eabf32e861d"), Name = "Server" },
                                new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("f59fa384-73b9-4f99-99dd-7eabf32e861d"), Name = "WASM" },
                                new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("f59fa384-73b9-4f99-99dd-7eabf32e861d"), Name = "MAUI(mobile development)" }
                            }
                        },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("35f7d762-8ae8-450f-ae4a-cd01e0b77ae1"), Name = "Bootstrap" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("35f7d762-8ae8-450f-ae4a-cd01e0b77ae1"), Name = "Basic React" }
                    }
                },
                new Tag
                {
                    Id = Guid.Parse("08f9c8e1-245f-43f5-bd39-543ae205c988"),
                    Name = "Backend",
                    Children = new()
                    {
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("08f9c8e1-245f-43f5-bd39-543ae205c988"), Name = "ASP.NET, .NET 5-8" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("08f9c8e1-245f-43f5-bd39-543ae205c988"), Name = "Dapper" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("08f9c8e1-245f-43f5-bd39-543ae205c988"), Name = "Minimal API, REST API" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("08f9c8e1-245f-43f5-bd39-543ae205c988"), Name = "MSSQL: Stored Projedures, temporal tables, etc." }
                    }
                },
                new Tag
                {
                    Id = Guid.Parse("1ab7e432-cfdf-48e0-b9db-2a453728d294"),
                    Name = "Languages",
                    Children = new()
                    {
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("1ab7e432-cfdf-48e0-b9db-2a453728d294"), Name = "C#" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("1ab7e432-cfdf-48e0-b9db-2a453728d294"), Name = "JavaScript" },
                        new Tag { Id = Guid.NewGuid(), ParentId = Guid.Parse("1ab7e432-cfdf-48e0-b9db-2a453728d294"), Name = "CSSS3/SASS" }
                    }
                },
                new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = "GitHub/GitLab"
                }
            };

            _projects = new()
            {
                new ProjectHighlight
                {
                    Title = "📦 ParcelLocker Dashboard",
                    Description = "Real-time data visualization of current data using Highcharts and Blazor. Includes dynamic filtering, responsive grid layout, and performance stats.",
                    Tags = new[] { "Blazor", "Highcharts", "SQL" },
                    TagColors = new[] { "success", "secondary", "dark" }
                },
                new ProjectHighlight
                {
                    Title = "🧠 Tag Manager",
                    Description = "Custom-built tag tree (like the one above!) to manage recursive tagging with add/edit/delete functionality.",
                    Tags = new[] { "Blazor", "UX Logic", "Component Design" },
                    TagColors = new[] { "info text-dark", "warning text-dark", "primary" }
                },
                new ProjectHighlight
                {
                    Title = "🌍 Multi-Company Statistic Portal",
                    Description = "Enterprise tool for viewing and exporting usage statistics across partnered companies. Custom filtering logic, export-to-Excel, and beautiful charts.",
                    Tags = new[] { "Entity Framework", "REST API", "Blazor Server", "SQL" },
                    TagColors = new[] { "success", "danger", "dark", "dark" }
                }
            };
        }

        private List<Tag> SelectedTags = new();

        private void HandleSelectionChanged(List<Tag> selected)
        {
            SelectedTags = selected;
        }

        private void HandleDelete(Tag tag)
        {
            RemoveTagRecursive(Tags, tag);
        }

        private bool RemoveTagRecursive(List<Tag> list, Tag tagToRemove)
        {
            var itemToRemove = list.FirstOrDefault(i => i.Id == tagToRemove.Id);
            if (itemToRemove != null)
            {
                list.Remove(itemToRemove);
                return true;
            }

            foreach (var tag in list)
            {
                if (RemoveTagRecursive(tag.Children, tagToRemove))
                    return true;
            }

            return false;
        }

        private void HandleRename((Tag item, string newName) rename)
        {
            rename.item.Name = rename.newName;
            StateHasChanged();
        }

        private void HandleAddChild((Tag parent, string childName) add)
        {
            if (!string.IsNullOrWhiteSpace(add.childName))
            {
                add.parent.Children.Add(new Tag
                {
                    Id = Guid.NewGuid(),
                    ParentId = add.parent.Id,
                    Name = add.childName,
                    Children = new()
                });
                StateHasChanged();
            }
        }
    }
}
