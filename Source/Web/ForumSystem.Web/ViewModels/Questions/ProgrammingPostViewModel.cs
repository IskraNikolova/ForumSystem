﻿namespace ForumSystem.Web.ViewModels.Questions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class ProgrammingPostViewModel : IMapFrom<Post>
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Answers")]
        public IList<Answer> Answers { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }
    }
}