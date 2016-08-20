namespace ForumSystem.Web.ViewModels.Home
{
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }
    }
}