namespace ForumSystem.Web.ViewModels.Answers
{
    using System;
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

        [Display(Name = "RatingUp")]
        public int RatingUp { get; set; }

        [Display(Name = "RatingDown")]
        public int RatingDown { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}