namespace ForumSystem.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Common.Repository;
    using ForumSystem.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
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

        public FileContentResult UserPhotos()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                string userId = this.User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = this.HttpContext.Server.MapPath(@"~/Images/noImg.png");
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int) imageFileLength);

                    return this.File(imageData, "image/png");
                }

                var bdUsers = this.HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.FirstOrDefault(x => x.Id == userId);

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                //todo method
                string fileName = this.HttpContext.Server.MapPath(@"~/Images/noImg.png");
                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return this.File(imageData, "image/png");
            }
        }
    }
}
