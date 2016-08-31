namespace ForumSystem.Web.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Home;

    public class GetFirstPosts
    {
        private const int DefaultLength = 50;

        public static IEnumerable<IndexBlogPostViewModel> GetFirstIndexPosts(IList<IndexBlogPostViewModel> all, int length = DefaultLength)
        {
            int realLength = Math.Min(all.Count, length);
            var result = all.OrderByDescending(p => p.CreatedOn).Take(realLength);
            return result;
        }
    }
}
