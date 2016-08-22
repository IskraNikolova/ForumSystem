namespace ForumSystem.Web.InputModels.Question
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ForumSystem.Models;

    public class AskInputModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Content")]
        [DataType("tinymce_full")]
        public string Content { get; set; }

        //todo crearte validator
        [Display(Name = "Tags")]
        public string Tags { get; set; }

        public ApplicationUser User { get; set; }
    }
}