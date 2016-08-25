namespace ForumSystem.Web.InputModels.Question
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class AskInputModel: IMapFrom<Post>
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        [DataType("tinymce_full")]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }
    }
}