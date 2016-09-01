namespace ForumSystem.Web.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Security;
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
                if (answer.RatingPoint % 10 == 0)
                {
                    answer.Author.Points += 1000;
                }

                answer.Author.Points += 100;
            }


            this.answers.SaveChanges();
            return this.RedirectToAction("ViewReadMore", "Questions", new { id = answer.PostId });
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
                user.Points -= 100;
            }


            this.answers.SaveChanges();
            return this.RedirectToAction("ViewReadMore", "Questions", new { id = answer.PostId });
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateAnswerInputModel();

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id,CreateAnswerInputModel input)
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
                return this.RedirectToAction("ViewReadMore", "Questions", new { id = answer.PostId });
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

        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = this.answers
                .All()
                .FirstOrDefault(a => a.Id == id);

            this.answers.Delete(answer);
            this.answers.SaveChanges();

            return this.RedirectToAction("ViewReadMore", "Questions", new { id = answer.PostId });
        }

        public ActionResult ViewAnswersOfPost(int id)
        {
            var answers = this.answers
                .All()
                .Where(a => a.PostId == id)
                .Project()
                .To<AnswerViewModel>();

            return this.View(answers);
        }
    }
}