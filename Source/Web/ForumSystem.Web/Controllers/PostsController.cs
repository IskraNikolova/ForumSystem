namespace ForumSystem.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Common.Repository;
    using ForumSystem.Models;

    public class PostsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;

        public PostsController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        // GET: Posts
        public ActionResult Index()
        {
            return this.View(this.posts.All().Include(p => p.Author).ToList());
        }
    }
}