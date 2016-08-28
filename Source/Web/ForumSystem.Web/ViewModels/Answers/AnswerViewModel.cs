namespace ForumSystem.Web.ViewModels.Answers
{
    using System.ComponentModel.DataAnnotations;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public AnswerViewModel()
        {           
        }

        public AnswerViewModel(int postId)
        {
            this.PostId = postId;
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Post")]
        public Post Post { get; set; }

        [Display(Name = "PostId")]
        public int PostId { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }
    }
}