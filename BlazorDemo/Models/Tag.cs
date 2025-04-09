namespace BlazorDemo.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public List<Tag> Children { get; set; } = new();
    }
}
