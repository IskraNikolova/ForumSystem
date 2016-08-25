namespace ForumSystem.Web.InputModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class AnswerInputModel : IMapFrom<Answer>
    {
        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        [DataType("tinymce_full")]
        public string Content { get; set; }

        [Display(Name = "PostId")]
        public int PostId { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Display(Name = "Post")]
        public Post Post { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }
    }
}