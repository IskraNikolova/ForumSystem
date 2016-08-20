namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Data.Common.Repository;
    using ForumSystem.Models;

    public class HomeController : Controller
    {
        private IRepository<Post> posts;

        public HomeController(IRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var allPosts = this.posts.All();
            return this.View(allPosts);
        }
    }
}