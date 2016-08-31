namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Runtime.Remoting.Channels;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNetCore.Http;
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
            Tag programmingTag = new Tag
            {
                Name = "programming",
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            Tag musicTag = new Tag
            {
                Name = "music",
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            Tag sportTag = new Tag
            {
                Name = "sport",
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            if (!context.Users.Any())
            {
                this.CreateUser(context, "admin@gmail.com", "123456", "System Administrator");
                this.CreateUser(context, "sis@yahoo.com", "123456", "Sisi Radeva");
                this.CreateUser(context, "dany@yahoo.com", "123456", "Dani Nikolov");

                this.CreateRole(context, "Administrators");
                this.AddUserToRole(context, "admin@gmail.com", "Administrators");

                this.CreateTag(context, "fun", false, DateTime.Now);
                this.CreateTag(context, "other", false, DateTime.Now);

                this.CreatePost(context,
                    title: "Work Begins on HTML5.1",
                    content: @"<p>The World Wide Web Consortium (W3C) has begun work on <b>HTML5.1</b>, and this time it is handling the creation of the standard a little differently. The specification has its <b><a href=""https://w3c.github.io/html/"">own GitHub project</a></b> where anyone can see what is happening and propose changes.</p>
                    <p>The organization says the goal for the new specification is to <b>match reality better</b>, to make the specification as clear as possible to readers, and of course to make it possible for all stakeholders to propose improvements, and understand what makes changes to HTML successful.""</p>
                    <p>Creating HTML5 took years, but W3C hopes using GitHub will speed up the process this time around. It plans to release a candidate recommendation for HTML5.1 by <b>June</b> and a full recommendation in <b>September</b>.</p>",
                    tag: programmingTag,
                    authorUsername: "dany@yahoo.com"
                );

                this.CreatePost(context,
                    title: "Windows 10 Preview with Bash Support Now Available",
                    content: @"<p>Microsoft has released a new <b>Windows 10 Insider Preview</b> that includes native support for <b>Bash running on Ubuntu Linux</b>. The company first announced the new feature at last week''s Build development conference, and it was one of the biggest stories of the event. The current process for installing Bash is a little complication, but Microsoft has a blog post that explains how the process works.</p>
                    <p>The preview build also includes <b>Cortana</b> upgrades, extensions support, the new <b>Skype</b> Universal Windows Platform app and some interface improvements.</p>",
                     tag: programmingTag,
                    authorUsername: "sis@yahoo.com"
                );

                this.CreatePost(context,
                    title: "Quisque et mauris a est posuere varius sit amet non neque",
                    content: @"<div>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc nulla diam, viverra in turpis eget, lobortis aliquam dui. Pellentesque a metus neque. Vivamus diam enim, feugiat ac nisi sed, auctor maximus metus. Ut convallis libero a felis finibus aliquet sit amet sed libero. Aenean lobortis auctor augue quis condimentum. Etiam lobortis dolor id metus fermentum ultrices. Nulla rutrum lectus est. Praesent dictum quam sit amet erat volutpat, eu suscipit ante mattis. Pellentesque id tellus non nulla mattis ornare. Donec nunc sapien, ornare sit amet orci sed, pellentesque pretium sem. Etiam efficitur mi elit, nec venenatis ligula gravida at. Integer id lectus ipsum. Sed vitae auctor sem. Sed in ullamcorper lectus. Quisque et mauris a est posuere varius sit amet non neque.

Donec nibh sem, sagittis at quam at, convallis fermentum tellus. Sed lacinia consectetur augue id imperdiet. Etiam sed erat iaculis, gravida velit sed, convallis erat. Nam congue, ipsum nec suscipit pulvinar, dui dolor pellentesque neque, non posuere leo erat sed magna. Fusce nec odio a nisi feugiat commodo vitae sed metus. Quisque fermentum accumsan sollicitudin. Aenean justo mauris, ultrices vitae vehicula in, facilisis vel nulla. Integer non diam turpis. Donec non nisl finibus, vehicula magna in, scelerisque sapien. Duis ornare eros orci, id facilisis quam pretium at. In blandit dignissim vestibulum. Cras enim purus, venenatis non diam vitae, pellentesque maximus libero.

Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus tempus purus dolor, at tristique dolor lobortis vel. Ut congue gravida nulla in scelerisque. Quisque ligula felis, ultricies lacinia condimentum sed, mollis sit amet massa. Vivamus pellentesque tristique tempor. Integer finibus purus sed auctor pharetra. Sed molestie eros neque, id consectetur lorem posuere quis. Maecenas nec lorem ut nisl venenatis scelerisque at in lectus. Vestibulum cursus orci eu sapien rutrum, ac convallis ipsum dignissim. Etiam vitae arcu pulvinar, condimentum quam condimentum, ultricies orci. Morbi egestas, mauris eu molestie sodales, nisi magna molestie augue, nec interdum sapien augue vitae risus.

Phasellus vitae euismod leo. Ut venenatis rhoncus leo, vitae tincidunt ligula euismod eget. Mauris cursus, turpis id luctus scelerisque, ex ligula dapibus libero, id pulvinar nunc ex quis metus. Aenean vel dolor leo. Aenean viverra, dolor ut luctus viverra, justo purus aliquet eros, ut porttitor felis dolor eu elit. Pellentesque at ligula scelerisque felis porttitor pellentesque. Suspendisse et auctor sapien, vitae blandit elit. Nulla facilisi. Donec at purus magna.

Pellentesque vel tellus tortor. Duis euismod tincidunt quam, eget faucibus lorem efficitur at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec blandit eget mauris ut fringilla. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nulla erat nisi, posuere in ex sed, auctor dictum urna. Aenean cursus nulla vel risus molestie, sed eleifend quam ornare. Nulla tortor diam, rutrum id massa sed, efficitur fringilla elit. Nunc sed scelerisque magna, lacinia interdum purus. Nam pretium iaculis tellus, non varius magna maximus ac. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean lacinia lobortis nibh, in volutpat enim </div>",
                    tag: musicTag,
                    authorUsername: "admin@gmail.com"
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
                    tag: programmingTag,
                    authorUsername: "dany@yahoo.com"
                );

                this.CreatePost(context,
              title: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
              content: @"<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean finibus varius rutrum. Ut nec nulla vitae quam feugiat posuere id vel purus. Nulla vitae consequat est. Aliquam eros urna, condimentum suscipit erat at, ornare dictum lacus. Donec auctor massa sit amet ligula blandit, id aliquet est maximus. Aliquam erat volutpat. Curabitur aliquet porta lectus, consequat hendrerit leo luctus quis. Aliquam pellentesque, elit nec pharetra aliquet, mi nibh eleifend arcu, a convallis justo nibh ac ex. Maecenas tincidunt ex dignissim dolor fermentum molestie. Cras aliquam quam mauris, eget lobortis leo blandit ut. Nunc ac justo id tortor congue bibendum vel non urna. Integer ut sapien ut nisi efficitur congue. Nullam bibendum est at justo vehicula posuere. Maecenas lacinia libero ligula, at finibus odio fermentum a. Pellentesque placerat pretium libero nec sodales. Vestibulum eu pharetra dui.</p>

                 <p>Integer efficitur magna at ex lobortis consectetur. Donec ullamcorper, est in dapibus molestie, diam tellus condimentum turpis, id porttitor tellus dui id urna. Vestibulum aliquam, arcu id eleifend semper, leo leo consectetur ex, lobortis dapibus quam nibh et mi. Ut nisl lectus, aliquet sit amet orci elementum, ultrices euismod ipsum. Integer gravida dui ut tortor pellentesque, at iaculis ex rutrum. Phasellus rutrum tempor ipsum quis ornare. Proin et finibus elit. Ut sed arcu pellentesque justo viverra porttitor. Duis posuere vestibulum elementum. Suspendisse vitae fermentum elit. Maecenas nec elit venenatis, fringilla lorem ut, scelerisque sem. Fusce ultricies porttitor lacus vitae varius. Donec nec iaculis velit. Nullam imperdiet felis in augue laoreet mollis. Praesent eleifend, sem blandit faucibus iaculis, ipsum est dignissim mauris, a porttitor est erat et diam.</p>

                  <p>Sed lobortis rhoncus orci, sed sodales elit vestibulum et. Quisque id risus eu lectus porttitor vehicula. Aliquam et mi semper velit dictum laoreet. Suspendisse id libero vel turpis finibus accumsan vitae at nibh. Mauris imperdiet quis ex non lacinia. Donec tempus eleifend orci, in facilisis quam ornare at. Aenean fringilla, libero non lacinia faucibus, elit ex posuere dui, id consectetur velit nisi commodo lectus. Morbi viverra vestibulum pellentesque. Praesent vel dignissim massa. Ut lacinia blandit nibh quis pharetra.</p>

                 <p>Nunc nec nisl leo. Aenean gravida ornare fringilla. Duis aliquam sem a urna porttitor, mollis pharetra nunc cursus. Pellentesque ac tortor nec magna lobortis euismod ac id mi. Cras luctus arcu dictum elit mattis, fermentum posuere nisi volutpat. Sed at imperdiet tortor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>

                 <p>Morbi risus mauris, feugiat sed enim vitae, bibendum sagittis lectus. Nullam et mauris a mauris ullamcorper rhoncus. Morbi feugiat ligula eget odio pharetra, eget consequat est mollis. Duis neque urna, viverra a condimentum eu, porta sit amet diam. Duis venenatis nulla nec turpis sodales ornare. Nulla eu mi consequat, venenatis ante et, ultrices massa. Nullam condimentum malesuada elit, eget ultricies elit egestas varius.</p>
                 <p>It is a big step ahead. Now SoftUni offers several professions:</p>",
              tag: sportTag,
              authorUsername: "dany@yahoo.com"
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
                FullName = fullName,
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
           string name, bool isDeleted, DateTime createdOn)
        {
            var tag = new Tag();
            tag.Name = name;
            tag.IsDeleted = isDeleted;
            tag.CreatedOn = createdOn;
            context.Tags.Add(tag);
        }

        private void CreatePost(ApplicationDbContext context,
            string title, string content, Tag tag, string authorUsername)
        {
            var post = new Post();
            post.Title = title;
            post.Content = content;
            post.Tag = tag;
            post.Author = context.Users.FirstOrDefault(u => u.UserName == authorUsername);
            context.Posts.Add(post);
        }
    }
}
