namespace ForumSystem.Web.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
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
                          .To<AnswerViewModel>()
                          .FirstOrDefault();

            if (answerViewModel == null)
            {
                return this.HttpNotFound("Not found answer.");
            }

            return this.View(answerViewModel);
        }

        [Authorize]
        public ActionResult Rate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Answer answer = this.answers
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (answer == null)
            {
                return this.HttpNotFound();
            }

            return this.View(answer);
        }

        [Authorize]
        [HttpPost, ActionName("Rate")]
        [ValidateAntiForgeryToken]
        public ActionResult RateConfirmed(int id)
        {
            var user = this.users.All().
                FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            Answer answer = this.answers               
                .All()
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);

            if (answer.RatingUpUsers == null)
            {
                answer.RatingUpUsers = string.Empty;
            }

            if (!answer.RatingUpUsers.Contains(user.UserName))
            {
                answer.RatingUp++;
                answer.RatingPoint++;
                answer.RatingUpUsers += user.UserName;
            }


            this.answers.SaveChanges();
            return this.RedirectToAction("ViewAll", new {id = answer.PostId});
        }

        [Authorize]
        public ActionResult RateDown(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Answer answer = this.answers
                .All()
                .FirstOrDefault(a => a.Id == id);

            if (answer == null)
            {
                return this.HttpNotFound();
            }

            return this.View(answer);
        }

        [Authorize]
        [HttpPost, ActionName("RateDown")]
        [ValidateAntiForgeryToken]
        public ActionResult RateDownConfirmed(int id)
        {
            var user = this.users.All().
                FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            Answer answer = this.answers
                .All()
                .Include(a => a.Author)
                .FirstOrDefault(a => a.Id == id);

            if (answer.RatingDownUsers == null)
            {
                answer.RatingDownUsers = string.Empty;
            }

            if (!answer.RatingDownUsers.Contains(user.UserName))
            {
                answer.RatingDown++;
                answer.RatingPoint--;
                answer.RatingDownUsers += user.UserName;
            }


            this.answers.SaveChanges();
            return this.RedirectToAction("ViewAll", new { id = answer.PostId });
        }

        public ActionResult ViewAll(int id)
        {
            var viewAnswers = this.answers.All()
                 .Where(a => a.PostId == id)
                 .Project()
                 .To<AnswerViewModel>();

            return this.View(viewAnswers);
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
            var answerAuthor = this.users
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            if (answerAuthor != null)
            {
                this.users.Detach(answerAuthor);
            }

            if (this.ModelState.IsValid)
            {
                var answer = new Answer
                {
                    Content = this.sanitizer.Sanitize(input.Content),
                    PostId = id,
                    Author = answerAuthor
                };

                this.answers.Add(answer);
                this.answers.SaveChanges();
                return this.RedirectToAction("ViewAll");
            }

            return this.View(input);
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Answer answer = this.answers.All().FirstOrDefault(a => a.Id == id);
            if (answer == null)
            {
                return this.HttpNotFound();
            }

            return this.View(answer);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = this.answers
                .All()
                .FirstOrDefault(a => a.Id == id);

            this.answers.Delete(answer);
            this.answers.SaveChanges();
            return this.RedirectToAction("ViewAll", new { id = answer.PostId });
        }
    }
}