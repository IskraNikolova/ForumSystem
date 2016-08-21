namespace ForumSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using InputModels.Question;

    public class QuestionsController : Controller
    {
        private IDeletableEntityRepository<Post> posts;

        public QuestionsController(IDeletableEntityRepository<Post> posts)
        {
            this.posts = posts;
        }

        public ActionResult Display(int id, string url, int page = 1)
        {
            return this.Content(id + " " + url);
        }

        public ActionResult GetByTag(string tag)
        {
            return this.Content(tag);
        }

        [HttpGet]
        public ActionResult Ask()
        {
            var model = new AskInputModel();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Ask(AskInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = input.Title,
                    Content = input.Content
                    //TODO tags
                    //TODO author
                };

                this.posts.Add(post);

                this.posts.SaveChanges();
                return this.RedirectToAction("Display", new {id = post.Id, url = "new"});
            }

            return this.View(input);
        }
    }
}