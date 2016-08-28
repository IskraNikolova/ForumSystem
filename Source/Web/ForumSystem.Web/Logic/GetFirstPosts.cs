namespace ForumSystem.Web.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using ForumSystem.Models;
    using ViewModels.Home;

    public class GetFirstPosts
    {
        private const int DefaultLength = 6;

        public static IEnumerable<IndexBlogPostViewModel> GetFirstIndexPosts(IList<IndexBlogPostViewModel> all, int length = DefaultLength)
        {

            var result = all.Take(length);
            return result;
        }
    }
}
