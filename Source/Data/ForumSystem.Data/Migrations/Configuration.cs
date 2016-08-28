namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;


    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            //ToDo Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                this.CreateUser(context, "admin@gmail.com", "123456", "System Administrator");
                this.CreateUser(context, "pesho@gmail.com", "123456", "Peter Ivanov");
                this.CreateUser(context, "merry@gmail.com", "123456", "Maria Petrova");
                this.CreateUser(context, "geshu@gmail.com", "123456", "George Petrov");

                this.CreateRole(context, "Administrators");
                this.AddUserToRole(context, "admin@gmail.com", "Administrators");

                this.CreateTag(context, "music");
                this.CreateTag(context, "sport");
                this.CreateTag(context, "programming");
                this.CreateTag(context, "fun");
                this.CreateTag(context, "other");

                this.CreatePost(context,
                    title: "Work Begins on HTML5.1",
                    content: @"<p>The World Wide Web Consortium (W3C) has begun work on <b>HTML5.1</b>, and this time it is handling the creation of the standard a little differently. The specification has its <b><a href=""https://w3c.github.io/html/"">own GitHub project</a></b> where anyone can see what is happening and propose changes.</p>
                    <p>The organization says the goal for the new specification is ""to <b>match reality better</b>, to make the specification as clear as possible to readers, and of course to make it possible for all stakeholders to propose improvements, and understand what makes changes to HTML successful.""</p>
                    <p>Creating HTML5 took years, but W3C hopes using GitHub will speed up the process this time around. It plans to release a candidate recommendation for HTML5.1 by <b>June</b> and a full recommendation in <b>September</b>.</p>",
                    tag: "programming",
                    authorUsername: "merry@gmail.com"
                );

                this.CreatePost(context,
                    title: "Windows 10 Preview with Bash Support Now Available",
                    content: @"<p>Microsoft has released a new <b>Windows 10 Insider Preview</b> that includes native support for <b>Bash running on Ubuntu Linux</b>. The company first announced the new feature at last week''s Build development conference, and it was one of the biggest stories of the event. The current process for installing Bash is a little complication, but Microsoft has a blog post that explains how the process works.</p>
                    <p>The preview build also includes <b>Cortana</b> upgrades, extensions support, the new <b>Skype</b> Universal Windows Platform app and some interface improvements.</p>",
                    tag: "programming",
                    authorUsername: "merry@gmail.com"
                );

                this.CreatePost(context,
                    title: "Atom Text Editor Gets New Windows Features",
                    content: @"<p>GitHub has released <b>Atom 1.7</b>, and the updated version of the text editor offers improvements for Windows developers. Specifically, it is now easier to build in Visual Studio, and it now supports the Appveyor CI continuous integration service for Windows.</p>
                    <p>Other new features include improved tab switching, tree view and crash recovery. GitHub noted, ""Crashes are nobody''s idea of fun, but in case Atom does crash on you, it periodically saves your editor state. After relaunching Atom after a crash, you should find all your work saved and ready to go.""</p>
                    <p>GitHub has also released a beta preview of Atom 1.8.</p>",
                    tag: "programming",
                    authorUsername: "merry@gmail.com"
                );

                this.CreatePost(context,
                    title: "SoftUni 3.0 Just Launched",
                    content: @"<p>The <b>Software University (SoftUni)</b> launched a new training methodology and training program for software engineers in Sofia.</p>
                    <p>It is a big step ahead. Now SoftUni offers several professions:</p>
                    <ul>
                      <li>PHP Developer</li>
                      <li>JavaScript Developer</li>
                      <li>C# Web Developer</li>
                      <li>Java Web Developer</li>
                    </ul>",
                    tag: "programming",
                    authorUsername: "pesho@gmail.com"
                );

                context.SaveChanges();
            }
        }

        private void CreateUser(ApplicationDbContext context,
            string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }
        private void CreateTag(ApplicationDbContext context,
           string name)
        {
            var tag = new Tag();
            tag.Name = name;
            context.Tags.Add(tag);
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string content, string tag, string authorUsername)
        {
            var post = new Post();
            post.Title = title;
            post.Content = content;
            post.Tag = context.Tags.FirstOrDefault(t => t.Name == tag);
            post.Author = context.Users.FirstOrDefault(u => u.UserName == authorUsername);
            context.Posts.Add(post);
        }
    }
}
