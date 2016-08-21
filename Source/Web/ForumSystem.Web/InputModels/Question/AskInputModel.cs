namespace ForumSystem.Web.InputModels.Question
{
    using System.ComponentModel.DataAnnotations;

    public class AskInputModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }
    }
}