namespace ForumSystem.Web.ViewModels.Answers
{
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public ApplicationUser Author { get; set; }
    }
}