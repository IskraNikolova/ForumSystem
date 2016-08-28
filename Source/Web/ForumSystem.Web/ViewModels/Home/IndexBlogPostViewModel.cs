namespace ForumSystem.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class IndexBlogPostViewModel : IMapFrom<Post>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Tag")]
        public Tag Tag { get; set; }

        [Display(Name = "Answers")]
        public IList<Answer> Answers { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }
    }
}