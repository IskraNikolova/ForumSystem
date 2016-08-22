namespace ForumSystem.Web.ViewModels.Questions
{
    using ForumSystem.Models;
    using Infrastructure.Mapping;
    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public ApplicationUser Author { get; set; }
    }
}