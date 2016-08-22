namespace ForumSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using ForumSystem.Models;
    using Infrastructure;
    using Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public IList<Tag> Tags { get; set; }

        public ApplicationUser Author { get; set; }
    }
}