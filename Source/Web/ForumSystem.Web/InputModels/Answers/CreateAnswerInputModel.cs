namespace ForumSystem.Web.InputModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class CreateAnswerInputModel :IMapFrom<Answer>
    {
        [AllowHtml]
        [Display(Name = "Content")]
        [DataType("tinymce_full")]
        public string Content { get; set; }
    }
}