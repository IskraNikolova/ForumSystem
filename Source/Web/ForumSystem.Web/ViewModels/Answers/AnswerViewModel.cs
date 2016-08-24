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

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Post Post { get; set; }

        public int PostId { get; set; }

        public ApplicationUser Author { get; set; }
    }
}