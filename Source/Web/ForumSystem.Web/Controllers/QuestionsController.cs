namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using Infrastructure;
    using InputModels.Question;
    using Microsoft.Ajax.Utilities;
    using ViewModels.Questions;

    public class QuestionsController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly ISanitizer sanitizer;

        public QuestionsController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<ApplicationUser> users, ISanitizer sanitizer)
        {
            this.posts = posts;
            this.users = users;
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
        public ActionResult Ask()
        {
            var model = new AskInputModel();
            
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Ask(AskInputModel input)
        {
            var tags = input.Tags.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
                       
            List<Tag> myTags = new List<Tag>();
            foreach (var tag in tags)
            {
                var myTag = new Tag
                {
                    Name = tag
                };

                myTags.Add(myTag);
            }

            var author = this.users.All().Where(u => u.UserName == this.User.Identity.Name).FirstOrDefault();

            if (this.ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = input.Title,
                    Content = this.sanitizer.Sanitize(input.Content),
                    Tags = myTags,
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