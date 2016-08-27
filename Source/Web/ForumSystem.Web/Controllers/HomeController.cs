namespace ForumSystem.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data.Common.Repository;
    using ForumSystem.Models;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<Answer> answers;

        public HomeController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<Answer> answers)
        {
            this.posts = posts;
            this.answers = answers;
        }

        public ActionResult Index()
        {
            var allposts = this.posts.All()
                  .Include(p => p.Author)
                  .Include(p => p.Answers)
                  .ToList();

            return this.View(allposts);
        }
    }
}
