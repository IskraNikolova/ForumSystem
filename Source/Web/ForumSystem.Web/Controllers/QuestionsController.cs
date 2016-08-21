namespace ForumSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using Infrastructure;
    using InputModels.Question;
    using ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly ISanitizer sanitizer;

        public QuestionsController(IDeletableEntityRepository<Post> posts, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        public ActionResult Display(int id, string url, int page = 1)
        {
            var postViewModel = this.posts
                .All()
                .Where(p => p.Id == id)
                .Project()
                .To<QuestionDisplayViewModel>().FirstOrDefault();

            if (postViewModel == null)
            {
                return this.HttpNotFound("Not found post.");
            }

            return this.View(postViewModel);
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
                    Content = this.sanitizer.Sanitize(input.Content),
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