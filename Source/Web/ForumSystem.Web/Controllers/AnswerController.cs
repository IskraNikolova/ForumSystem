namespace ForumSystem.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using Infrastructure;
    using InputModels.Answers;
    using ViewModels.Answers;

    public class AnswerController : Controller
    {
        private readonly IDeletableEntityRepository<Answer> answers;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Post> posts;

        private readonly ISanitizer sanitizer;

        public AnswerController(IDeletableEntityRepository<Answer> answers,
            IDeletableEntityRepository<ApplicationUser> users,
            IDeletableEntityRepository<Post> posts,
            ISanitizer sanitizer)
        {
            this.answers = answers;
            this.users = users;
            this.posts = posts;
            this.sanitizer = sanitizer;
        }

        public ActionResult Display(int id, string url = "", int page = 1)
        {
            var answerViewModel = this.answers
                          .All()
                          .Include(p => p.Author)
                          .Where(a => a.Id == id)
                          .Project()
                          .To<AnswerViewModel>().FirstOrDefault();

            if (answerViewModel == null)
            {
                return this.HttpNotFound("Not found post.");
            }

            return this.View(answerViewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var model = new AnswerInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, AnswerInputModel input)
        {
            var aswerAuthor = this.users
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);
            if (aswerAuthor != null)
            {
                this.users.Detach(aswerAuthor);
            }

            if (this.ModelState.IsValid)
            {
                var answer = new Answer
                {
                    Content = this.sanitizer.Sanitize(input.Content),
                    PostId = id,
                    Author = aswerAuthor
                };

                this.answers.Add(answer);
                this.answers.SaveChanges();
                return this.RedirectToAction("Display", new { id = answer.Id, url = "new" });
            }

            return this.View(input);
        }

        public ActionResult All(int id, int maxComments = 999, int startFrom = 0)
        {
            var comments = this.answers
                .All()
                .Where(c => c.PostId == id && !c.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(startFrom)
                .Take(maxComments)
                .Project()
                .To<AnswerViewModel>();

            return this.PartialView(comments);
        }
    }
}