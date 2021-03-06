﻿namespace ForumSystem.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ForumSystem.Models;
    using Infrastructure.Mapping;

    public class QuestionDisplayViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public int Points { get; set; }

        [Display(Name = "Tag")]
        public Tag Tag { get; set; }

        [Display(Name = "Answers")]
        public IList<Answer> Answers { get; set; }

        [Display(Name = "Author")]
        public ApplicationUser Author { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}