namespace ForumSystem.Web.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Home;

    public class GetFirstPosts
    {
        private const int DefaultLength = 6;

        public static IEnumerable<IndexBlogPostViewModel> GetFirstIndexPosts(IList<IndexBlogPostViewModel> all, int length = DefaultLength)
        {

            var result = all.OrderByDescending(p => p.CreatedOn).Take(length);
            return result;
        }
    }
}
