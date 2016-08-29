namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IDeletableEntityRepository<Post> posts;
        private readonly IDeletableEntityRepository<ApplicationUser> users;

        public HomeController(IDeletableEntityRepository<Post> posts, IDeletableEntityRepository<ApplicationUser> users)
        {
            this.posts = posts;
            this.users = users;
        }

        public ActionResult Index()
        {
            var allPosts = this.posts.All()
                    .Project().To<IndexBlogPostViewModel>()
                    .ToList();

            var allUsers = this.users.All().ToList();
            var modelForIndexPage = new Tuple<IList<ApplicationUser>, IList<IndexBlogPostViewModel>>(allUsers, allPosts);
            return this.View(modelForIndexPage);
        }
    }
}
