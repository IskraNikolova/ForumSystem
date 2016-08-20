namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private IRepository<Post> posts;

        public HomeController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var allPosts = this.posts.All().Project().To<IndexBlogPostViewModel>();
            return this.View(allPosts);
        }
    }
}