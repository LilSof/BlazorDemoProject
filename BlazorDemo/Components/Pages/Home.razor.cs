using BlazorDemo.Models;

namespace BlazorDemo.Components.Pages
{
    public partial class Home
    {
        private List<Tag> _tags = new();
        private List<ProjectHighlight> _projects = new();

        protected override void OnInitialized()
        {
            _tags = new()
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
                    Tags = new[] { "Blazor Server", "Highcharts", "SQL" },
                    TagColors = new[] { "success", "secondary", "dark" }
                },
                new ProjectHighlight
                {
                    Title = "🧠 Tag Manager",
                    Description = "Custom-built tag tree (like the one above!) to manage recursive tagging with add/edit/delete functionality.",
                    Tags = new[] { "Blazor Server", "UX Logic", "Component Design" },
                    TagColors = new[] { "info text-dark", "warning text-dark", "primary" }
                },
                new ProjectHighlight
                {
                    Title = "🌍 Multi-Company Statistic Portal",
                    Description = "Enterprise tool for viewing and exporting usage statistics across partnered companies. Custom filtering logic, export-to-Excel, and beautiful charts.",
                    Tags = new[] { "Dapper", "REST API", "Blazor Server", "SQL" },
                    TagColors = new[] { "success", "danger", "dark", "dark" }
                }
            };
        }

        private void HandleDelete(Tag tag)
        {
            if (!RemoveTagRecursive(_tags, tag))
            {
                Console.WriteLine("Tag not found for deletion.");
            }
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
            try
            {
                var tagToRename = FindTagById(_tags, rename.item.Id);
                if (tagToRename != null)
                {
                    tagToRename.Name = rename.newName;
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Tag not found for renaming.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Rename error: {ex.Message}");
            }
        }

        private Tag? FindTagById(List<Tag> list, Guid id)
        {
            foreach (var tag in list)
            {
                if (tag.Id == id)
                    return tag;

                var found = FindTagById(tag.Children, id);
                if (found != null)
                    return found;
            }
            return null;
        }

        private void HandleAddChild((Tag parent, string childName) add)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(add.childName))
                {
                    var parentTag = FindTagById(_tags, add.parent.Id);
                    if (parentTag != null)
                    {
                        parentTag.Children.Add(new Tag
                        {
                            Id = Guid.NewGuid(),
                            ParentId = parentTag.Id,
                            Name = add.childName,
                            Children = new()
                        });
                        StateHasChanged();
                    }
                    else
                    {
                        Console.WriteLine("Parent tag not found for adding child.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddChild error: {ex.Message}");
            }
        }
    }

}
