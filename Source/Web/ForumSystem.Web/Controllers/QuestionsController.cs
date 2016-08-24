namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
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
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Tag> tags;
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

        public ActionResult Display(int id, string url = "", int page = 1)
        {
            var postViewModel = this.posts
                                    .All()
                                    .Include(p => p.Author)
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
            List<Post> postsWithSameTag = new List<Post>();
            ////var tagMy = new Tag
            ////{
            ////    Name = tag,
            ////};

            ////foreach (var post in this.posts.All())
            ////{
            ////    if (post.Tags.Contains(tagMy))
            ////    {
            ////        postsWithSameTag.Add(post);
            ////    }
            ////}

            return this.Content(string.Join("\n", postsWithSameTag));
        }

        [HttpGet]
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
            List<Tag> tags2 = new List<Tag>();
            if (input.Tags != null)
            {
                var tags1 = input.Tags.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in tags1)
                {
                    var myTag = this.tags.All().FirstOrDefault(t => t.Name == tag.Trim());
                    if (myTag != null)
                    {
                        this.tags.Detach(myTag);
                        tags2.Add(myTag);
                    }
                }
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
                    Tags = tags2,
                    Author = author
                };

                this.posts.Add(post);
                this.posts.SaveChanges();
                return this.RedirectToAction("Display", new {id = post.Id, url = "new"});
            }

            return this.View(input);
        }
    }
}