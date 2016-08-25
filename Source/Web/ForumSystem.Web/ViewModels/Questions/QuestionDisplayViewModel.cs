namespace ForumSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Tag Tag { get; set; }

        public ApplicationUser Author { get; set; }
    }
}