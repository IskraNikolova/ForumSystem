namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using Infrastructure;
    using InputModels.Question;
    using ViewModels.Home;
    using ViewModels.Questions;
    using WebGrease.Css.Extensions;

    public class QuestionsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Tag> tags;
        private readonly IDeletableEntityRepository<Answer> answers;
        private readonly ISanitizer sanitizer;

        public QuestionsController(IDeletableEntityRepository<Post> posts,
            IDeletableEntityRepository<ApplicationUser> users,
            IDeletableEntityRepository<Tag> tags,
            ISanitizer sanitizer)
        {
            this.posts = posts;
            this.users = users;
            this.tags = tags;
            this.sanitizer = sanitizer;
        }
        public ActionResult Index()
        {
            var allPosts = this.posts.All()
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedOn)
                .Project()
                .To<AllCategoriesViewModel>();

            return this.View(allPosts);
        }

        public ActionResult Display(int id, string url = "", int page = 1)
        {
            var postViewModel = this.posts
                .All()
                .Include(p => p.Author)
                .Where(p => p.Id == id)
                .Project()
                .To<QuestionDisplayViewModel>()
                .FirstOrDefault();

            if (postViewModel == null)
            {
                return this.HttpNotFound("Not found post.");
            }

            return this.View(postViewModel);
        }

        public ActionResult ViewReadMore(int id)
        {
            var viewQuestion = this.posts.All()
                .Include(p => p.Tag)  
                .Include(p => p.Answers) 
                .Where(p => p.Id == id)
                .Project()
                .To<QuestionDisplayViewModel>()
                .FirstOrDefault();


            return this.View(viewQuestion);
        }

        [Authorize]
        public ActionResult Ask()
        {
            var model = new AskInputModel();

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Ask(AskInputModel input)
        {
            var tag = this.tags.All()
                .FirstOrDefault(t => t.Name == input.Tag);

            if (tag == null)
            { 
                tag = this.tags.All()
                .FirstOrDefault(t => t.Name == "other");
            }

            var author = this.users
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            if (author != null)
            {
                this.users.Detach(author);
            }

            if (this.ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = input.Title,
                    Content = this.sanitizer.Sanitize(input.Content),
                    Tag = tag,
                    Author = author
                };

                this.posts.Add(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("Display", new {id = post.Id, url = "new"});
            }

            return this.View(input);
        }

        [Authorize(Roles = "Administrators")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return this.HttpNotFound();
            }

            return this.View(post);
        }

        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Post post = this.posts
                .All()
                .FirstOrDefault(p => p.Id == id);

                this.posts.Delete(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("Index");
        }

        
        public ActionResult Music()
        {
            var allPosts = this.posts.All()
                .Where(p => p.Tag.Name == "music")
                .Project().To<MusicPostViewModel>();


            return this.View(allPosts);
        }

        public ActionResult Sport()
        {
            var allPosts = this.posts.All()
                .Where(p => p.Tag.Name == "sport")
                .Project()
                .To<SportPostViewModel>();

            return this.View(allPosts);
        }

        public ActionResult Fun()
        {
            var allPosts = this.posts.All()
               .Where(p => p.Tag.Name == "fun")
               .Project()
               .To<IndexPostViewModel>();

            return this.View(allPosts);
        }

        public ActionResult Programming()
        {
            var allPosts = this.posts.All()
                    .Where(p => p.Tag.Name == "programming")
                    .Project()
                    .To<ProgrammingPostViewModel>();

            return this.View(allPosts);
        }
    }
}