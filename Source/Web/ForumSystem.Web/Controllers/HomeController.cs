namespace ForumSystem.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public HomeController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            var allposts = this.posts.All()
                    .Project().To<IndexBlogPostViewModel>()
                    .ToList();

            return this.View(allposts);
        }
    }
}
