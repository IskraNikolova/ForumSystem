namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private IDeletableEntityRepository<Post> posts;

        public HomeController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            this.posts.Delete(6);
            this.posts.SaveChanges();

            var allPosts = this.posts.All().Project().To<IndexBlogPostViewModel>();
            return this.View(allPosts);
        }
    }
}