using System.Diagnostics.CodeAnalysis;

namespace ThirdPartyApiDemo.Models
{
    [ExcludeFromCodeCoverage]
    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }
}
